import { ParamBase } from './param.model';

export interface EventStore {
  id: string;
  streamId: string;
  stringName: string;
  eventType: string;
  data: unknown;
  revision: number;
}

export interface EventStoreParam extends ParamBase {
  streamId?: string;
  stringName?: string;
}
