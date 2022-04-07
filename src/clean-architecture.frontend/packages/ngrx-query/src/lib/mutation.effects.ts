import { WindowManager } from './window-manager.service';
import { selectQueryFeature } from './query.selectors';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Action, Store } from '@ngrx/store';
import { map, groupBy, mergeMap, switchMap, catchError, withLatestFrom, Observable } from 'rxjs';

import * as MutationActions from './mutation.actions';
import { getMutation } from './utils';

@Injectable()
export class MutationEffects {
  mutate$: Observable<Action> = createEffect(() => this.actions$.pipe(
    ofType(MutationActions.mutate),
    groupBy(a => a.mutationKey),
    mergeMap(mutations$ => mutations$.pipe(
      withLatestFrom(this.store$.select(selectQueryFeature)),
      map(([a, state]) => getMutation(state, a.mutationKey)),
      switchMap(mutation => mutation.mutationFn(mutation.state.variables).pipe(
        mergeMap(data => [MutationActions.succeeded({ mutationKey: mutation.mutationKey, data })]),
        catchError(error => [MutationActions.failed({ mutationKey: mutation.mutationKey, error })]),
      )),
    )),
  ));

  constructor(
    private actions$: Actions,
    private store$: Store,
    private windowManager: WindowManager,
  ) {}
}

