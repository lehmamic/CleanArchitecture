import { Mutation } from './mutation.state';
import { QueryFunction, QueryKey, QueryStatus } from "./types";

export const QUERY_FEATURE_KEY = 'NGRX_QUERY';

export interface WindowState {
  isFocused: boolean;
  isOnline: boolean;
}

export interface GlobalOptionsState {
  refetchInterval: number;
  refetchIntervalInBackground: boolean;
  refetchOnWindowFocus: boolean;
  refetchOnReconnect: boolean;
  staleTime: number;
  cacheTime: number;
}

export interface Query<TData = unknown, TError = unknown> {
  queryKey: QueryKey;
  queryHash: string;
  queryFn: QueryFunction<TData>;
  options: QueryOptionsState;
  state: QueryState<TData, TError>;
};

export interface QueryOptionsState {
  refetchInterval: number;
  refetchIntervalInBackground: boolean;
  refetchOnWindowFocus: boolean;
  refetchOnReconnect: boolean;
  staleTime: number;
  cacheTime: number;
}

export interface QueryState<TData = unknown, TError = unknown> {
  data: TData | undefined;
  // dataUpdateCount: number
  dataUpdatedAt: number;
  error: TError | null;
  // errorUpdateCount: number
  errorUpdatedAt: number;
  fetchFailureCount: number;
  refCount: number;
  // fetchMeta: any
  isFetching: boolean;
  isInvalidated: boolean;
  isPaused: boolean;
  status: QueryStatus;
};

export interface HashMap<TValue> {
  [hash: string]: TValue;
};

export interface QueryFeatureState {
  options: GlobalOptionsState;
  window: WindowState;
  queries: HashMap<Query>;
  mutations: HashMap<Mutation>;
}

export const initialQueryState: QueryFeatureState = {
  options: {
    refetchInterval: 0,
    refetchIntervalInBackground: false,
    refetchOnWindowFocus: true,
    refetchOnReconnect: true,
    staleTime: 0,
    cacheTime: 30000,
  },
  window: {
    isFocused: [undefined, 'visible', 'prerender'].includes(document?.visibilityState),
    isOnline: navigator?.onLine ?? true,
  },
  queries: {},
  mutations: {},
};
