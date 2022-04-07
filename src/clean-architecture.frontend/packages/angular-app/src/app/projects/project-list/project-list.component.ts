import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ProjectDto } from '@clean-architecture.frontend/api-angular';
import { ProjectsService } from '../projects.service';

@Component({
  selector: 'clean-architecture.frontend-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {

  public projects!: Observable<ProjectDto[] | undefined>;

  constructor(private projectsService: ProjectsService) { }

  ngOnInit(): void {
    ({ data: this.projects } = this.projectsService.getProjects());
  }

}
