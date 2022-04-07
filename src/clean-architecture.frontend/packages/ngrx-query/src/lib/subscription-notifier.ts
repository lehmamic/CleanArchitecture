import { Observable, Subscription, PartialObserver } from 'rxjs';

export function subscriptionNotify<T>(
  onSubscribed: (count: number) => void,
  onUnsubscribed: (count: number) => void,
): (source: Observable<T>) => Observable<T> {
  return (source: Observable<T>): Observable<T> => {
    let subscriptionCounter: number = 0;

    return new Observable((observer: PartialObserver<T>): (() => void) => {
      const innerSubscription: Subscription = source.subscribe(observer);
      subscriptionCounter += 1;

      onSubscribed(subscriptionCounter);

      return (): void => {
        innerSubscription.unsubscribe();
        subscriptionCounter -= 1;

        onUnsubscribed(subscriptionCounter);
      };
    });
  };
}
