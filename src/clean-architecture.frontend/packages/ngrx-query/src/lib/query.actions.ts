import { QueryConfiguration } from './query.configuration';
import { QueryState } from './query.state';
import { createAction, props } from '@ngrx/store';
import { QueryFunction, QueryKey, QueryOptions } from './types';

export const setGlobalConfig = createAction(
  '[QUERY] Set Global Configuration',
  props<{ config: QueryConfiguration }>(),
);

export const create = createAction(
  '[QUERY] Create Query',
  props<{ queryKey: QueryKey, queryFn: QueryFunction, queryOpt: QueryOptions }>(),
);

export const fetch = createAction(
  '[QUERY] Fetch Query',
  props<{ queryKey: QueryKey }>(),
);

export const succeeded = createAction(
  '[QUERY] Fetch Query Succeeded',
  props<{ queryKey: QueryKey, data: unknown }>(),
);

export const failed = createAction(
  '[QUERY] Fetch Query Failed',
  props<{ queryKey: QueryKey, error: unknown }>(),
);

export const incrementRefCount = createAction(
  '[QUERY] Increment Query Ref Count',
  props<{ queryKey: QueryKey }>(),
);

export const decrementRefCount = createAction(
  '[QUERY] Decrement Query Ref Count',
  props<{ queryKey: QueryKey }>(),
);

export const setState = createAction(
  '[QUERY] Set Query State',
  props<{ queryKey: QueryKey, state: QueryState }>(),
);

export const refetch = createAction(
  '[QUERY] Refetch Query',
  props<{ queryKey: QueryKey }>(),
);

export const refetchOnInterval = createAction(
  '[QUERY] Refetch Query On Interval',
  props<{ queryKey: QueryKey }>(),
);

export const refetchPeriodic = createAction(
  '[QUERY] Refetch Query Periodic',
  props<{ queryKey: QueryKey }>(),
);

export const invalidate = createAction(
  '[QUERY] Invalidate Query',
  props<{ queryKey?: QueryKey }>(),
);

export const setQueryData = createAction(
  '[QUERY] Set Query Data',
  props<{ queryKey: QueryKey, data: unknown }>(),
);

export const mutate = createAction('[QUERY] Mutate');
