/* tslint:disable */
/* eslint-disable */
/**
 * CleanArchitecture.Web
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import * as runtime from '../runtime';
import {
    CreateProjectCommand,
    CreateProjectCommandFromJSON,
    CreateProjectCommandToJSON,
    ProjectDto,
    ProjectDtoFromJSON,
    ProjectDtoToJSON,
    UpdateProjectCommand,
    UpdateProjectCommandFromJSON,
    UpdateProjectCommandToJSON,
} from '../models';

export interface GetProjectRequest {
    id: string;
}

export interface ProjectsIdDeleteRequest {
    id: string;
}

export interface ProjectsIdPutRequest {
    id: string;
    updateProjectCommand?: UpdateProjectCommand;
}

export interface ProjectsPostRequest {
    createProjectCommand?: CreateProjectCommand;
}

/**
 * 
 */
export class ProjectsApi extends runtime.BaseAPI {

    /**
     */
    async getProjectRaw(requestParameters: GetProjectRequest, initOverrides?: RequestInit): Promise<runtime.ApiResponse<ProjectDto>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling getProject.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        const response = await this.request({
            path: `/Projects/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => ProjectDtoFromJSON(jsonValue));
    }

    /**
     */
    async getProject(requestParameters: GetProjectRequest, initOverrides?: RequestInit): Promise<ProjectDto> {
        const response = await this.getProjectRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async projectsGetRaw(initOverrides?: RequestInit): Promise<runtime.ApiResponse<Array<ProjectDto>>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        const response = await this.request({
            path: `/Projects`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => jsonValue.map(ProjectDtoFromJSON));
    }

    /**
     */
    async projectsGet(initOverrides?: RequestInit): Promise<Array<ProjectDto>> {
        const response = await this.projectsGetRaw(initOverrides);
        return await response.value();
    }

    /**
     */
    async projectsIdDeleteRaw(requestParameters: ProjectsIdDeleteRequest, initOverrides?: RequestInit): Promise<runtime.ApiResponse<ProjectDto>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling projectsIdDelete.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        const response = await this.request({
            path: `/Projects/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => ProjectDtoFromJSON(jsonValue));
    }

    /**
     */
    async projectsIdDelete(requestParameters: ProjectsIdDeleteRequest, initOverrides?: RequestInit): Promise<ProjectDto> {
        const response = await this.projectsIdDeleteRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async projectsIdPutRaw(requestParameters: ProjectsIdPutRequest, initOverrides?: RequestInit): Promise<runtime.ApiResponse<ProjectDto>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling projectsIdPut.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        const response = await this.request({
            path: `/Projects/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'PUT',
            headers: headerParameters,
            query: queryParameters,
            body: UpdateProjectCommandToJSON(requestParameters.updateProjectCommand),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => ProjectDtoFromJSON(jsonValue));
    }

    /**
     */
    async projectsIdPut(requestParameters: ProjectsIdPutRequest, initOverrides?: RequestInit): Promise<ProjectDto> {
        const response = await this.projectsIdPutRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async projectsPostRaw(requestParameters: ProjectsPostRequest, initOverrides?: RequestInit): Promise<runtime.ApiResponse<ProjectDto>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        const response = await this.request({
            path: `/Projects`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: CreateProjectCommandToJSON(requestParameters.createProjectCommand),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => ProjectDtoFromJSON(jsonValue));
    }

    /**
     */
    async projectsPost(requestParameters: ProjectsPostRequest, initOverrides?: RequestInit): Promise<ProjectDto> {
        const response = await this.projectsPostRaw(requestParameters, initOverrides);
        return await response.value();
    }

}
