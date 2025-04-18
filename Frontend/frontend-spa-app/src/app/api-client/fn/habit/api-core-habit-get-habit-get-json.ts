/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { HabitDto } from '../../models/habit-dto';

export interface ApiCoreHabitGetHabitGet$Json$Params {
  habitId?: string;
}

export function apiCoreHabitGetHabitGet$Json(http: HttpClient, rootUrl: string, params?: ApiCoreHabitGetHabitGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<HabitDto>> {
  const rb = new RequestBuilder(rootUrl, apiCoreHabitGetHabitGet$Json.PATH, 'get');
  if (params) {
    rb.query('habitId', params.habitId, {});
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<HabitDto>;
    })
  );
}

apiCoreHabitGetHabitGet$Json.PATH = '/api/core/Habit/get-habit';
