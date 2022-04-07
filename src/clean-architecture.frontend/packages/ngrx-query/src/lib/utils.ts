import { Mutation } from './mutation.state';
import { Query, QueryFeatureState, QueryOptionsState, QueryState } from './query.state';
import { EnsuredQueryKey, QueryFunction, QueryKey, QueryOptions, MutationKey, MutationFunction, MutationOptions } from "./types"

export function isPlainObject(o: any): o is object {
  if (!hasObjectPrototype(o)) {
    return false
  }

  // If has modified constructor
  const ctor = o.constructor
  if (typeof ctor === 'undefined') {
    return true
  }

  // If has modified prototype
  const prot = ctor.prototype
  if (!hasObjectPrototype(prot)) {
    return false
  }

  // If constructor does not have an Object-specific method
  // eslint-disable-next-line no-prototype-builtins
  if (!prot.hasOwnProperty('isPrototypeOf')) {
    return false
  }

  // Most likely a plain Object
  return true
}

function hasObjectPrototype(o: any): boolean {
  return Object.prototype.toString.call(o) === '[object Object]'
}

export function isQueryKey(value: any): value is QueryKey {
  return typeof value === 'string' || Array.isArray(value)
}

export function ensureQueryKeyArray<T extends QueryKey>(
  value: T
): EnsuredQueryKey<T> {
  return (Array.isArray(value)
    ? value
    : ([value] as unknown)) as EnsuredQueryKey<T>
}

export function hashQueryKey(queryKey: QueryKey): string {
  const asArray = ensureQueryKeyArray(queryKey)
  return stableValueHash(asArray)
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export function stableValueHash(value: any): string {
  return JSON.stringify(value, (_, val: { [key: string]: unknown}) =>
    isPlainObject(val)
      ? Object.keys(val)
          .sort()
          .reduce((result, key) => {
            result[key] = val[key]
            return result
          // eslint-disable-next-line @typescript-eslint/no-explicit-any
          }, {} as any)
      : val
  )
}

export function timeUntilStale(updatedAt: number, staleTime?: number): number {
  return Math.max(updatedAt + (staleTime || 0) - Date.now(), 0)
}

export function isStale(query: Query): boolean {
  return (
    query.state.isInvalidated ||
    !query.state.dataUpdatedAt
  );
}

export function isStaleByTime(query: Query, staleTime = 0): boolean {
  return (
    query.state.isInvalidated ||
    !query.state.dataUpdatedAt ||
    !timeUntilStale(query.state.dataUpdatedAt, staleTime)
  )
}

export function isActive(query: Query): boolean {
  return query.state.refCount > 0 && !query.state.isPaused;
}

export function buildQuery<TData = unknown, TError = unknown>(state: QueryFeatureState, queryKey: QueryKey, queryFn: QueryFunction<TData>, queryOptions: QueryOptions): Query<TData, TError> {
  return {
    queryKey,
    queryHash: hashQueryKey(queryKey),
    queryFn,
    options: {
      refetchInterval: queryOptions.refetchInterval ?? state.options.refetchInterval,
      refetchIntervalInBackground: queryOptions.refetchIntervalInBackground ?? state.options.refetchIntervalInBackground,
      refetchOnWindowFocus: queryOptions.refetchOnWindowFocus ?? state.options.refetchOnWindowFocus,
      refetchOnReconnect: queryOptions.refetchOnReconnect ?? state.options.refetchOnReconnect,
      staleTime: queryOptions.staleTime ?? state.options.staleTime,
      cacheTime: queryOptions.cacheTime ?? state.options.cacheTime,
    },
    state: {
      data: undefined,
      dataUpdatedAt: 0,
      error: null,
      errorUpdatedAt: 0,
      fetchFailureCount: 0,
      refCount: 0,
      isFetching: false,
      isInvalidated: true,
      isPaused: false,
      status: 'idle',
    },
  };
}

export function getQuery<TData = unknown, TError = unknown>(state: QueryFeatureState, queryKey: QueryKey): Query<TData, TError> {
  const queryHash = hashQueryKey(queryKey)
  const query = state.queries[queryHash];

  return <Query<TData, TError>>query;
}

export function getOrBuildQuery<TData = unknown, TError = unknown>(state: QueryFeatureState, { queryKey, queryFn, queryOpt }: {queryKey: QueryKey; queryFn: QueryFunction<TData>; queryOpt: QueryOptions; }): Query<TData, TError> {
  let query = getQuery<TData, TError>(state, queryKey);

  if (!query) {
    query = buildQuery<TData, TError>(state, queryKey, queryFn, queryOpt);
  }

  return query;
};

export function getMatchingQueryKeys(state: QueryFeatureState,queryKey?: QueryKey): QueryKey[] {
  return queryKey ? [queryKey] : Object.keys(state.queries).map(key => state.queries[key].queryKey);
}

export function buildMutation<TData = unknown, TError = unknown, TVariables = unknown>(state: QueryFeatureState, mutationKey: MutationKey, mutationFn: MutationFunction<TData, TVariables>, mutationOptions: MutationOptions): Mutation<TData, TError, TVariables> {
  return {
    mutationKey,
    mutationHash: hashQueryKey(mutationKey),
    mutationFn,
    options: {
      ...mutationOptions,
    },
    state: {
      data: undefined,
      error: null,
      failureCount: 0,
      isPaused: false,
      status: 'idle',
      variables: undefined,
    },
  };
}

export function getMutation<TData = unknown, TError = unknown, TVariables = unknown>(state: QueryFeatureState, mutationKey: MutationKey): Mutation<TData, TError, TVariables> {
  const mutationHash = hashQueryKey(mutationKey)
  const mutation = state.mutations[mutationHash];

  return <Mutation<TData, TError, TVariables>>mutation;
}

export function getOrBuildMutation<TData = unknown, TError = unknown, TVariables = unknown>(state: QueryFeatureState, { mutationKey, mutationFn, mutationOpt }: {mutationKey: MutationKey; mutationFn: MutationFunction<TData, TVariables>; mutationOpt: MutationOptions; }): Mutation<TData, TError, TVariables> {
  let mutation = getMutation<TData, TError, TVariables>(state, mutationKey);

  if (!mutation) {
    mutation = buildMutation<TData, TError, TVariables>(state, mutationKey, mutationFn, mutationOpt);
  }

  return mutation;
};

export function isError(query: Query | Mutation): boolean {
  return query.state.status == 'error';
}

export function isSuccess(query: Query | Mutation): boolean {
  return query.state.status == 'success';
}

export function isLoading(query: Query | Mutation): boolean {
  return query.state.status == 'loading';
}

export function isIdle(query: Query | Mutation): boolean {
  return query.state.status == 'idle';
}

export function isFetching(query: Query): boolean {
  return query.state.isFetching;
}

export function settled(prev: Query, curr: Query): boolean {
  return prev.state.isFetching && !curr.state.isFetching;
}

export function mutationSettled(prev: Mutation, curr: Mutation): boolean {
  return isLoading(prev) && !isLoading(curr);
}
