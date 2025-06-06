/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { AddDailyHabitDataCommand } from '../../models/add-daily-habit-data-command';
import { HabitDto } from '../../models/habit-dto';

export interface ApiCoreHabitDataAddHabitDataPost$Json$Params {
      body: AddDailyHabitDataCommand
}

export function apiCoreHabitDataAddHabitDataPost$Json(http: HttpClient, rootUrl: string, params: ApiCoreHabitDataAddHabitDataPost$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<HabitDto>> {
  const rb = new RequestBuilder(rootUrl, apiCoreHabitDataAddHabitDataPost$Json.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/*+json');
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

apiCoreHabitDataAddHabitDataPost$Json.PATH = '/api/core/HabitData/add-habit-data';
