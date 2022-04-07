import { QueryKey, MutationFunction } from './types';
import { createReducer, on } from '@ngrx/store';
import { initialQueryState } from "./query.state";

import * as QueryActions from './query.actions';
import * as MutationActions from './mutation.actions';
import { getQuery, getOrBuildQuery, getOrBuildMutation, getMatchingQueryKeys } from './utils';
import { QueryFeatureState, getMutation } from '..';

export const queryReducer = createReducer(
  initialQueryState,

  on(QueryActions.setGlobalConfig, (state, { config }) => ({
      ...state,
      options: {
        refetchInterval: config.refetchInterval ?? state.options.refetchInterval,
        refetchIntervalInBackground: config.refetchIntervalInBackground ?? state.options.refetchIntervalInBackground,
        refetchOnWindowFocus: config.refetchOnWindowFocus ?? state.options.refetchOnWindowFocus,
        refetchOnReconnect: config.refetchOnReconnect ?? state.options.refetchOnReconnect,
        staleTime: config.staleTime ?? state.options.staleTime,
        cacheTime: config.cacheTime ?? state.options.cacheTime,
      }
    }),
  ),

  on(QueryActions.create, (state, { queryKey, queryFn, queryOpt }) => {
    const query = getOrBuildQuery(state, { queryKey, queryFn, queryOpt });

    return {
      ...state,
      queries: {
        ...state.queries,
        [query.queryHash]: query,
      }
    };
  }),

  on(QueryActions.fetch, (state, { queryKey }) => {
    const query = getQuery(state, queryKey);
    const status = !query.state.dataUpdatedAt ? 'loading' : query.state.status;

    return {
      ...state,
      queries: {
        ...state.queries,
        [query.queryHash]: {
          ...query,
          state: { ...query.state, isFetching: true, status },
        },
      }
    };
  }),

  on(QueryActions.succeeded, (state, { queryKey, data }) => {
    const query = getQuery(state, queryKey);

    return {
      ...state,
      queries: {
        ...state.queries,
        [query.queryHash]: {
          ...query,
          state: { ...query.state, isFetching: false, status: 'success', data, dataUpdatedAt: Date.now(), error: null, isInvalidated: false },
        },
      }
    };
  }),

  on(QueryActions.failed, (state, { queryKey, error }) => {
    const query = getQuery(state, queryKey);

    return {
      ...state,
      queries: {
        ...state.queries,
        [query.queryHash]: {
          ...query,
          state: { ...query.state, isFetching: false, status: 'error', error, errorUpdatedAt: Date.now(), fetchFailureCount: query.state.fetchFailureCount + 1 },
        },
      }
    };
  }),

  on(QueryActions.incrementRefCount, (state, { queryKey }) => {
    const query = getQuery(state, queryKey);

    return {
      ...state,
      queries: {
        ...state.queries,
        [query.queryHash]: {
          ...query,
          state: { ...query.state, refCount: query.state.refCount + 1 },
        },
      }
    };
  }),

  on(QueryActions.decrementRefCount, (state, { queryKey }) => {
    const query = getQuery(state, queryKey);

    return {
      ...state,
      queries: {
        ...state.queries,
        [query.queryHash]: {
          ...query,
          state: { ...query.state, refCount: query.state.refCount - 1 },
        },
      }
    };
  }),

  on(QueryActions.invalidate, (state, { queryKey }) => {
    const invalidateQuery = (featureState: QueryFeatureState, queryKey: QueryKey): QueryFeatureState => {
      const query = getQuery(featureState, queryKey);

      return {
        ...featureState,
        queries: {
          ...featureState.queries,
          [query.queryHash]: {
            ...query,
            state: { ...query.state, isInvalidated: true, },
          },
        }
      };
    };

    return getMatchingQueryKeys(state, queryKey)
      .reduce((prevState, key) => invalidateQuery(prevState, key), state);
  }),

  on(QueryActions.setState, (state, { queryKey, state: queryState }) => {
    const query = getQuery(state, queryKey);

    return {
      ...state,
      queries: {
        ...state.queries,
        [query.queryHash]: {
          ...query,
          state: queryState,
        },
      }
    };
  }),

  on(MutationActions.create, (state, { mutationKey, mutationFn, mutationOpt }) => {
    const mutation = getOrBuildMutation(state, { mutationKey, mutationFn, mutationOpt });

    return {
      ...state,
      mutations: {
        ...state.mutations,
        [mutation.mutationHash]: mutation,
      }
    };
  }),

  on(MutationActions.mutate, (state, { mutationKey, variables }) => {
    const mutation = getMutation(state, mutationKey);

    return {
      ...state,
      mutations: {
        ...state.mutations,
        [mutation.mutationHash]: {
          ...mutation,
          state: { ...mutation.state, status: 'loading', variables },
        },
      }
    };
  }),

  on(MutationActions.succeeded, (state, { mutationKey, data }) => {
    const mutation = getMutation(state, mutationKey);

    return {
      ...state,
      mutations: {
        ...state.mutations,
        [mutation.mutationHash]: {
          ...mutation,
          state: { ...mutation.state, status: 'success', data, error: null },
        },
      }
    };
  }),

  on(MutationActions.failed, (state, { mutationKey, error }) => {
    const mutation = getMutation(state, mutationKey);

    return {
      ...state,
      mutations: {
        ...state.mutations,
        [mutation.mutationHash]: {
          ...mutation,
          state: { ...mutation.state, isFetching: false, status: 'error', error, failureCount: mutation.state.failureCount + 1 },
        },
      }
    };
  }),
);
