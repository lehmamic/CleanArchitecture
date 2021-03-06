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
 * @interface ToDoItemDto
 */
export interface ToDoItemDto {
    /**
     * 
     * @type {string}
     * @memberof ToDoItemDto
     */
    id?: string;
    /**
     * 
     * @type {string}
     * @memberof ToDoItemDto
     */
    title?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ToDoItemDto
     */
    description?: string | null;
    /**
     * 
     * @type {boolean}
     * @memberof ToDoItemDto
     */
    isDone?: boolean;
}

export function ToDoItemDtoFromJSON(json: any): ToDoItemDto {
    return ToDoItemDtoFromJSONTyped(json, false);
}

export function ToDoItemDtoFromJSONTyped(json: any, ignoreDiscriminator: boolean): ToDoItemDto {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'id': !exists(json, 'id') ? undefined : json['id'],
        'title': !exists(json, 'title') ? undefined : json['title'],
        'description': !exists(json, 'description') ? undefined : json['description'],
        'isDone': !exists(json, 'isDone') ? undefined : json['isDone'],
    };
}

export function ToDoItemDtoToJSON(value?: ToDoItemDto | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'id': value.id,
        'title': value.title,
        'description': value.description,
        'isDone': value.isDone,
    };
}

