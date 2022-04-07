export * from './items.service';
import { ItemsService } from './items.service';
export * from './projects.service';
import { ProjectsService } from './projects.service';
export const APIS = [ItemsService, ProjectsService];
