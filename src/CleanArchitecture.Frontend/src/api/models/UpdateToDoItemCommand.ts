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

import { exists, mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface UpdateToDoItemCommand
 */
export interface UpdateToDoItemCommand {
    /**
     * 
     * @type {string}
     * @memberof UpdateToDoItemCommand
     */
    id?: string;
    /**
     * 
     * @type {string}
     * @memberof UpdateToDoItemCommand
     */
    projectId?: string;
    /**
     * 
     * @type {string}
     * @memberof UpdateToDoItemCommand
     */
    title?: string | null;
    /**
     * 
     * @type {string}
     * @memberof UpdateToDoItemCommand
     */
    description?: string | null;
}

export function UpdateToDoItemCommandFromJSON(json: any): UpdateToDoItemCommand {
    return UpdateToDoItemCommandFromJSONTyped(json, false);
}

export function UpdateToDoItemCommandFromJSONTyped(json: any, ignoreDiscriminator: boolean): UpdateToDoItemCommand {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'id': !exists(json, 'id') ? undefined : json['id'],
        'projectId': !exists(json, 'projectId') ? undefined : json['projectId'],
        'title': !exists(json, 'title') ? undefined : json['title'],
        'description': !exists(json, 'description') ? undefined : json['description'],
    };
}

export function UpdateToDoItemCommandToJSON(value?: UpdateToDoItemCommand | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'id': value.id,
        'projectId': value.projectId,
        'title': value.title,
        'description': value.description,
    };
}

