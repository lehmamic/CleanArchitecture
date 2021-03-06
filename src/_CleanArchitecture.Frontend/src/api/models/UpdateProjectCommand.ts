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
 * @interface UpdateProjectCommand
 */
export interface UpdateProjectCommand {
    /**
     * 
     * @type {string}
     * @memberof UpdateProjectCommand
     */
    id?: string;
    /**
     * 
     * @type {string}
     * @memberof UpdateProjectCommand
     */
    name?: string | null;
}

export function UpdateProjectCommandFromJSON(json: any): UpdateProjectCommand {
    return UpdateProjectCommandFromJSONTyped(json, false);
}

export function UpdateProjectCommandFromJSONTyped(json: any, ignoreDiscriminator: boolean): UpdateProjectCommand {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'id': !exists(json, 'id') ? undefined : json['id'],
        'name': !exists(json, 'name') ? undefined : json['name'],
    };
}

export function UpdateProjectCommandToJSON(value?: UpdateProjectCommand | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'id': value.id,
        'name': value.name,
    };
}

