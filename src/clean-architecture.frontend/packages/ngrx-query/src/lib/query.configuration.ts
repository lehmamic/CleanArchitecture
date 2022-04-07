export interface QueryConfigurationParameters {
  refetchInterval?: number;
  refetchIntervalInBackground?: boolean;
  refetchOnWindowFocus?: boolean;
  refetchOnReconnect?: boolean;
  staleTime?: number;
  cacheTime?: number;
}

export class QueryConfiguration implements QueryConfigurationParameters{
  public refetchInterval?: number;
  public refetchIntervalInBackground?: boolean;
  public refetchOnWindowFocus?: boolean;
  public refetchOnReconnect?: boolean;
  public staleTime?: number;
  public cacheTime?: number;

  constructor(configurationParameters: QueryConfigurationParameters = {}) {
    ({
      refetchInterval: this.refetchInterval,
      refetchIntervalInBackground: this.refetchIntervalInBackground,
      refetchOnWindowFocus: this.refetchOnWindowFocus,
      refetchOnReconnect: this.refetchOnReconnect,
      staleTime: this.staleTime,
      cacheTime: this.cacheTime,
    } = configurationParameters);
  }
};
