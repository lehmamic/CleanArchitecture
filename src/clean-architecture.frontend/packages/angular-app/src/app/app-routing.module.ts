import { NgModule } from '@angular/core';
import { ExtraOptions, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'projects',
    pathMatch: 'full',
  },
  {
    path: 'projects',
    loadChildren: () => import('./projects/projects.module').then((m) => m.ProjectsModule),
  },
];

// eslint-disable-next-line one-var
export const routingConfiguration: ExtraOptions = {
  paramsInheritanceStrategy: 'always',
  initialNavigation: 'enabledBlocking',
};

@NgModule({
  imports: [RouterModule.forRoot(routes, routingConfiguration)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
