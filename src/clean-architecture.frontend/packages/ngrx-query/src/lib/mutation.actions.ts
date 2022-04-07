import { createAction, props } from '@ngrx/store';
import { MutationKey, MutationOptions, MutationFunction } from './types';

export const create = createAction(
  '[QUERY] Create Mutation',
  props<{ mutationKey: MutationKey, mutationFn: MutationFunction, mutationOpt: MutationOptions }>(),
);

export const mutate = createAction(
  '[QUERY] Call Mutation',
  props<{ mutationKey: MutationKey, variables: unknown }>(),
);

export const succeeded = createAction(
  '[QUERY] Call Mutation Succeeded',
  props<{ mutationKey: MutationKey, data: unknown }>(),
);

export const failed = createAction(
  '[QUERY] Call Mutation Failed',
  props<{ mutationKey: MutationKey, error: unknown }>(),
);
