import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { QueryClient, QueryResult } from '@clean-architecture.frontend/ngrx-query';
import { ProjectsService as ProjectsApi, ProjectDto } from '@clean-architecture.frontend/api-angular';

@Injectable({ providedIn: 'root' })
export class ProjectsService {
  constructor(private queryClient: QueryClient, private projects: ProjectsApi) {
  }

  public getProjects(): QueryResult<ProjectDto[]> {
    return this.queryClient.query<ProjectDto[], HttpErrorResponse>(['projects'], () => this.projects.apiV1ProjectsGet());
  }
}
