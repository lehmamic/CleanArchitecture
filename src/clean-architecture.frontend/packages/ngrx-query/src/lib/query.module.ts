import { MutationEffects } from './mutation.effects';
import { QUERY_FEATURE_KEY } from './query.state';
import { NgModule, Optional, SkipSelf, ModuleWithProviders } from '@angular/core';
import { QueryConfiguration, QueryConfigurationParameters } from './query.configuration';
import { Store, StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { QueryEffects } from './query.effects';
import { queryReducer } from './query.reducer';
import * as QueryActions from './query.actions';

@NgModule({
  imports: [
    StoreModule.forFeature(QUERY_FEATURE_KEY, queryReducer),
    EffectsModule.forFeature([QueryEffects, MutationEffects]),
  ],
  providers: [],
})
export class QueryModule {
  constructor(@Optional() @SkipSelf() parentModule: QueryModule, store$: Store, config: QueryConfiguration) {
    if (parentModule) {
      throw new Error('QueryModule is already loaded. Import in your base module only.');
    }

    store$.dispatch(QueryActions.setGlobalConfig({ config }));
  }

  public static forRoot(options: QueryConfigurationParameters = {}): ModuleWithProviders<QueryModule> {
    return {
        ngModule: QueryModule,
        providers: [ { provide: QueryConfiguration, useFactory: () => new QueryConfiguration(options) } ]
    };
  }
}
