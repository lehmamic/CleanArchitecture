import { Injectable } from '@angular/core';
import { selectQueryFeature } from './query.selectors';
import { Store } from '@ngrx/store';
import { map, distinctUntilChanged, filter, pairwise, last, switchMap } from 'rxjs';

import * as QueryActions from "./query.actions";
import * as MutationActions from './mutation.actions';
import { MutationFunction, QueryFunction, QueryKey, QueryOptions, QueryResult, MutationKey } from './types';
import { getQuery, mutationSettled, settled } from './utils';
import { isError, isLoading, isSuccess, isStaleByTime, MutationResult, getMutation, isIdle } from '..';
import { subscriptionNotify } from './subscription-notifier';
import { Mutation } from './mutation.state';

@Injectable({ providedIn: 'root' })
export class QueryClient {
  constructor(private store$: Store) {
  }

  public query<TData = unknown, TError = unknown, TQueryKey extends QueryKey = QueryKey>(queryKey: TQueryKey, queryFn: QueryFunction<TData>, queryOpt: QueryOptions = {}): QueryResult<TData, TError> {
    this.store$.dispatch(QueryActions.create({ queryKey, queryFn, queryOpt }));

    const query$ = this.store$
      .select(selectQueryFeature)
      .pipe(
        map(state => getQuery<TData, TError>(state, queryKey)),
        distinctUntilChanged(),
      );

    return {
      data: query$.pipe(
        map(query => query.state.data),
        subscriptionNotify(() => this.store$.dispatch(QueryActions.incrementRefCount({ queryKey })), () => QueryActions.decrementRefCount({ queryKey })),
      ),
      error: query$.pipe(
        map(query => query.state.error),
        subscriptionNotify(() => this.store$.dispatch(QueryActions.incrementRefCount({ queryKey })), () => QueryActions.decrementRefCount({ queryKey })),
      ),
      isError: query$.pipe(
        map(isError),
        subscriptionNotify(() => this.store$.dispatch(QueryActions.incrementRefCount({ queryKey })), () => QueryActions.decrementRefCount({ queryKey })),
      ),
      isLoading: query$.pipe(
        map(isLoading),
        subscriptionNotify(() => this.store$.dispatch(QueryActions.incrementRefCount({ queryKey })), () => QueryActions.decrementRefCount({ queryKey })),
      ),
      isFetching: query$.pipe(
        map(isLoading),
        subscriptionNotify(() => this.store$.dispatch(QueryActions.incrementRefCount({ queryKey })), () => QueryActions.decrementRefCount({ queryKey })),
      ),
      isStale: query$.pipe(
        map(q => isStaleByTime(q, q.options.cacheTime)), subscriptionNotify(() => this.store$.dispatch(QueryActions.incrementRefCount({ queryKey })), () => QueryActions.decrementRefCount({ queryKey }))),
      isSuccess: query$.pipe(
        map(isSuccess),
        subscriptionNotify(() => this.store$.dispatch(QueryActions.incrementRefCount({ queryKey })), () => QueryActions.decrementRefCount({ queryKey })),
      ),
      succeeded: query$.pipe(
        pairwise(),
        filter(([prev, curr]) => settled(prev, curr) && isSuccess(curr)),
        map(([, curr]) => curr.state.data),
        subscriptionNotify(() => this.store$.dispatch(QueryActions.incrementRefCount({ queryKey })), () => QueryActions.decrementRefCount({ queryKey })),
      ),
      failed: query$.pipe(
        pairwise(),
        filter(([prev, curr]) => settled(prev, curr) && isError(curr)),
        map(([, curr]) => curr.state.error),
        subscriptionNotify(() => this.store$.dispatch(QueryActions.incrementRefCount({ queryKey })), () => QueryActions.decrementRefCount({ queryKey })),
      ),
      settled: query$.pipe(
        pairwise(),
        filter(([prev, curr]) => settled(prev, curr)),
        map(([, curr]) => ({ data: curr.state.data, error: curr.state.error })),
        subscriptionNotify(() => this.store$.dispatch(QueryActions.incrementRefCount({ queryKey })), () => QueryActions.decrementRefCount({ queryKey })),
      ),
      status: query$.pipe(map(query => query.state.status)),
      invalidate: () => this.invalidateQueries(queryKey),
      setQueryData: (data: TData) => this.setQueryData(queryKey, data),
    };
  }

  public mutate<TData = unknown, TError = unknown, TVariables = unknown>(mutationFn: MutationFunction<TData, TVariables>): MutationResult<TData, TError, TVariables> {

    const mutationKey: MutationKey = 'aDS';
    this.store$.dispatch(MutationActions.create({ mutationKey, mutationFn: <MutationFunction>mutationFn, mutationOpt: {} }));

    const mutation$ = this.store$
      .select(selectQueryFeature)
      .pipe(
        map(state => getMutation<TData, TError, TVariables>(state, mutationKey)),
        distinctUntilChanged(),
      );

    return {
      data: mutation$.pipe(
        map(mutation => mutation.state.data),
      ),
      error: mutation$.pipe(
        map(mutation => mutation.state.error),
      ),
      isError: mutation$.pipe(
        map(mutation => isError(<Mutation>mutation)),
      ),
      isSuccess: mutation$.pipe(
        map(mutation => isSuccess(<Mutation>mutation)),
      ),
      isLoading: mutation$.pipe(
        map(mutation => isLoading(<Mutation>mutation)),
      ),
      isIdle: mutation$.pipe(
        map(mutation => isIdle(<Mutation>mutation)),
      ),
      succeeded: mutation$.pipe(
        pairwise(),
        filter(([prev, curr]) => mutationSettled(<Mutation>prev, <Mutation>curr) && isSuccess(<Mutation>curr)),
        map(([, curr]) => curr.state.data),
      ),
      failed: mutation$.pipe(
        pairwise(),
        filter(([prev, curr]) => mutationSettled(<Mutation>prev, <Mutation>curr) && isError(<Mutation>curr)),
        map(([, curr]) => curr.state.error),
      ),
      settled: mutation$.pipe(
        pairwise(),
        filter(([prev, curr]) => mutationSettled(<Mutation>prev, <Mutation>curr)),
        map(([, curr]) => ({ data: curr.state.data, error: curr.state.error })),
      ),
      status: mutation$.pipe(map(mutation => mutation.state.status)),
      mutate: (variables: TVariables) => mutation$.pipe(last(), switchMap(mutation => mutation.mutationFn(variables))),
    };
  }

  public invalidateQueries<TQueryKey extends QueryKey = QueryKey>(queryKey?: TQueryKey): void {
    this.store$.dispatch(QueryActions.invalidate({ queryKey }));
  }

  public setQueryData<TData = unknown, TQueryKey extends QueryKey = QueryKey>(queryKey: TQueryKey, data: TData): void {
    this.store$.dispatch(QueryActions.setQueryData({ queryKey, data }));
  }
}
