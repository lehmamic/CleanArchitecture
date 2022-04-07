import { WindowManager } from './window-manager.service';
import { isStaleByTime } from '..';
import { selectQueryFeature } from './query.selectors';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Action, Store } from '@ngrx/store';
import { map, groupBy, mergeMap, switchMap, catchError, withLatestFrom, Observable, interval, filter } from 'rxjs';

import * as QueryActions from './query.actions';
import { getMatchingQueryKeys, getQuery, isActive, isStale } from './utils';

@Injectable()
export class QueryEffects {
  create$: Observable<Action> = createEffect(() => this.actions$.pipe(
    ofType(QueryActions.create),
    groupBy(a => a.queryKey),
    mergeMap(queries$ => queries$.pipe(
      withLatestFrom(this.store$.select(selectQueryFeature)),
      map(([a, state]) => getQuery(state, a.queryKey)),
      filter(query => isStale(query)),
      mergeMap(query => [QueryActions.fetch({ queryKey: query.queryKey }), QueryActions.refetchOnInterval({ queryKey: query.queryKey })]),
    )),
  ));

  fetch$: Observable<Action> = createEffect(() => this.actions$.pipe(
    ofType(QueryActions.fetch),
    groupBy(a => a.queryKey),
    mergeMap(queries$ => queries$.pipe(
      withLatestFrom(this.store$.select(selectQueryFeature)),
      map(([a, state]) => getQuery(state, a.queryKey)),
      switchMap(query => query.queryFn().pipe(
        mergeMap(data => [QueryActions.succeeded({ queryKey: query.queryKey, data })]),
        catchError(error => [QueryActions.failed({ queryKey: query.queryKey, error })]),
      )),
    )),
  ));

  refetch$: Observable<Action> = createEffect(() => this.actions$.pipe(
    ofType(QueryActions.refetch),
    filter(() => this.windowManager.isOnline),
    withLatestFrom(this.store$.select(selectQueryFeature)),
    map(([a, state]) => getQuery(state, a.queryKey)),
    filter(query => isActive(query) && isStaleByTime(query, query.options.staleTime)),
    mergeMap(query => [QueryActions.fetch({ queryKey: query.queryKey})]),
  ));

  refetchOnInterval$: Observable<Action> = createEffect(() => this.actions$.pipe(
    ofType(QueryActions.refetchOnInterval),
    groupBy(a => a.queryKey),
    mergeMap(queries$ => queries$.pipe(
      withLatestFrom(this.store$.select(selectQueryFeature)),
      map(([a, state]) => getQuery(state, a.queryKey)),
      filter(query => query.options.refetchInterval > 0),
      switchMap(query => interval(query.options.refetchInterval).pipe(
        filter(() => this.windowManager.isFocused || query.options.refetchIntervalInBackground),
        mergeMap(() => [QueryActions.refetch({ queryKey: query.queryKey})]),
      )),
    )),
  ));

  refetchOnWindowFocus$: Observable<Action> = createEffect(() => this.actions$.pipe(
    ofType(QueryActions.refetchOnInterval),
    groupBy(a => a.queryKey),
    mergeMap(queries$ => queries$.pipe(
      withLatestFrom(this.store$.select(selectQueryFeature)),
      map(([a, state]) => getQuery(state, a.queryKey)),
      filter(query => query.options.refetchOnWindowFocus),
      switchMap(query => this.windowManager.focusChanged.pipe(
        filter(e => e.isFocused),
        mergeMap(() => [QueryActions.refetch({ queryKey: query.queryKey})]),
      )),
    )),
  ));

  refetchOnReconnect$: Observable<Action> = createEffect(() => this.actions$.pipe(
    ofType(QueryActions.refetchOnInterval),
    groupBy(a => a.queryKey),
    mergeMap(queries$ => queries$.pipe(
      withLatestFrom(this.store$.select(selectQueryFeature)),
      map(([a, state]) => getQuery(state, a.queryKey)),
      filter(query => query.options.refetchOnReconnect),
      switchMap(query => this.windowManager.onlineChanged.pipe(
        filter(e => e.isOnline),
        mergeMap(() => [QueryActions.refetch({ queryKey: query.queryKey})]),
      )),
    )),
  ));

  invalidate$: Observable<Action> = createEffect(() => this.actions$.pipe(
    ofType(QueryActions.invalidate),
    withLatestFrom(this.store$.select(selectQueryFeature)),
    mergeMap(([a, state]) => getMatchingQueryKeys(state, a.queryKey).map(queryKey => QueryActions.refetch({ queryKey}))),
  ));

  constructor(
    private actions$: Actions,
    private store$: Store,
    private windowManager: WindowManager,
  ) {}
}

