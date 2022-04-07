import { QueryModule } from '@clean-architecture.frontend/ngrx-query';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectsRoutingModule } from './projects-routing.module';

@NgModule({
  declarations: [
    ProjectListComponent,
  ],
  imports: [
    // Angular Modules
    CommonModule,

    // App Modules
    ProjectsRoutingModule,
  ],
  providers: [],
})
export class ProjectsModule {}
