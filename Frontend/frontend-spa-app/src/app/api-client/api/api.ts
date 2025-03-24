export * from './account.service';
import { AccountService } from './account.service';
export * from './healthCheck.service';
import { HealthCheckService } from './healthCheck.service';
export const APIS = [AccountService, HealthCheckService];
