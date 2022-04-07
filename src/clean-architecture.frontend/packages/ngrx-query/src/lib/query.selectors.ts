import { createFeatureSelector, createSelector } from '@ngrx/store';

import { QueryFeatureState, QUERY_FEATURE_KEY } from "./query.state";

export const selectQueryFeature = createFeatureSelector<QueryFeatureState>(QUERY_FEATURE_KEY);
