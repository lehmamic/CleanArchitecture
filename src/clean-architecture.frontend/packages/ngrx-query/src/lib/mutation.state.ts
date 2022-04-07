import { MutationFunction, MutationKey, MutationStatus } from "./types";

export interface MutationState<TData = unknown, TError = unknown, TVariables = unknown> {
  data: TData | undefined;
  error: TError | null;
  failureCount: number;
  isPaused: boolean;
  status: MutationStatus;
  variables: TVariables | undefined;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface MutationOptionsState {
}

export interface Mutation<TData = unknown, TError = unknown, TVariables = unknown> {
  mutationKey: MutationKey;
  mutationHash: string;
  mutationFn: MutationFunction<TData, TVariables>;
  options: MutationOptionsState;
  state: MutationState<TData, TError, TVariables>;
}
