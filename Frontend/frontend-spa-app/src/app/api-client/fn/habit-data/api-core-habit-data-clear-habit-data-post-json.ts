/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { HabitDto } from '../../models/habit-dto';
import { RemoveDailyHabitDataCommand } from '../../models/remove-daily-habit-data-command';

export interface ApiCoreHabitDataClearHabitDataPost$Json$Params {
      body: RemoveDailyHabitDataCommand
}

export function apiCoreHabitDataClearHabitDataPost$Json(http: HttpClient, rootUrl: string, params: ApiCoreHabitDataClearHabitDataPost$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<HabitDto>> {
  const rb = new RequestBuilder(rootUrl, apiCoreHabitDataClearHabitDataPost$Json.PATH, 'post');
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

apiCoreHabitDataClearHabitDataPost$Json.PATH = '/api/core/HabitData/clear-habit-data';
