import {IIdentity} from '@essential-projects/iam_contracts';

import {
  ExternalTask,
  IExternalTaskApi,
} from '@process-engine/external_task_api_contracts';

import {HttpClientConfig} from '../../../src/contracts/index';

export class ExternalTaskHttpClientMock implements IExternalTaskApi {

  public config: HttpClientConfig;

  public initialize(): void {}

  public async fetchAndLockExternalTasks<TPayloadType>(
    identity: IIdentity,
    workerId: string,
    topicName: string,
    maxTasks: number,
    longPollingTimeout: number,
    lockDuration: number,
  ): Promise<Array<ExternalTask<TPayloadType>>> {
    return Promise.resolve([]);
  }

  public async extendLock(identity: IIdentity, workerId: string, externalTaskId: string, additionalDuration: number): Promise<void> {
    return Promise.resolve();
  }

  public async handleBpmnError(identity: IIdentity, workerId: string, externalTaskId: string, errorCode: string): Promise<void> {
    return Promise.resolve();
  }

  public async handleServiceError(
    identity: IIdentity,
    workerId: string,
    externalTaskId: string,
    errorMessage: string,
    errorDetails: string,
  ): Promise<void> {
    return Promise.resolve();
  }

  public async finishExternalTask<TResultType>(identity: IIdentity, workerId: string, externalTaskId: string, results: TResultType): Promise<void> {
    return Promise.resolve();
  }

}