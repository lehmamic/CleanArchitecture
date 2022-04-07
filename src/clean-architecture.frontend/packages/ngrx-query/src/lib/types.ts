import { Observable } from 'rxjs';

export type QueryKey = string | readonly unknown[];

export type EnsuredQueryKey<T extends QueryKey> = T extends string? [T] : Exclude<T, string>

export type QueryFunction<T = unknown> = () => Observable<T>;

export type QueryStatus = 'idle' | 'loading' | 'error' | 'success';

export interface QueryResult<TData = unknown, TError = unknown> {
  data: Observable<TData | undefined>;
  error: Observable<TError | null>;
  isError: Observable<boolean>;
  isLoading: Observable<boolean>;
  isFetching: Observable<boolean>;
  isStale: Observable<boolean>;
  isSuccess: Observable<boolean>;
  succeeded: Observable<TData | undefined>;
  failed: Observable<TError | null>;
  settled: Observable<{ data: TData | undefined; error: TError | null }>;
  status: Observable<QueryStatus>;
  invalidate: () => void;
  setQueryData: (data: TData) => void;
};

export interface QueryOptions {
  refetchInterval?: number;
  refetchIntervalInBackground?: boolean;
  refetchOnWindowFocus?: boolean;
  refetchOnReconnect?: boolean;
  staleTime?: number;
  cacheTime?: number;
}

export type MutationKey = string | readonly unknown[];

export type MutationStatus = 'idle' | 'loading' | 'success' | 'error';

export type MutationFunction<TData = unknown, TVariables = unknown> = (variables: TVariables) => Observable<TData>;

export interface MutationResult<TData = unknown, TError = unknown, TVariables = unknown> {
  data: Observable<TData | undefined>;
  error: Observable<TError | null>;
  isError: Observable<boolean>;
  isIdle: Observable<boolean>;
  isLoading: Observable<boolean>;
  isSuccess: Observable<boolean>;
  succeeded: Observable<TData | undefined>;
  failed: Observable<TError | null>;
  settled: Observable<{ data: TData | undefined; error: TError | null }>;
  status: Observable<MutationStatus>
  mutate: MutationFunction<TData, TVariables>;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface MutationOptions {
}
