import { ParamBase } from './param.model';

export interface EventStore {
  id: string;
  streamId: string;
  streamName: string;
  eventType: string;
  data: any;
  revision: number;
}

export interface EventStoreParam extends ParamBase {
  streamId?: string;
  streamName?: string;
}
