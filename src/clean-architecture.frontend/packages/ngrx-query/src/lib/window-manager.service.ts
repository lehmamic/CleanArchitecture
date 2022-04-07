import { fromEvent, merge, Observable, map } from 'rxjs';
import { Injectable } from "@angular/core";

export interface FocusChangedEvent {
  isFocused: boolean;
}

export interface OnlineChangedEvent {
  isOnline: boolean;
}

function isFocused(): boolean {
  if (typeof document === 'undefined') {
    return true
  }

  return [undefined, 'visible', 'prerender'].includes(document.visibilityState);
}

function isOnline(): boolean {
  if (typeof navigator === 'undefined' || typeof navigator.onLine === 'undefined') {
    return true
  }

  return navigator.onLine
}

@Injectable({ providedIn: 'root' })
export class WindowManager {

  public get onlineChanged(): Observable<OnlineChangedEvent> {
    return merge(
      fromEvent(window, 'online').pipe(map(() => ({ isOnline: true }))),
      fromEvent(window, 'offline').pipe(map(() => ({ isOnline: false }))),
    );
  }

  public get isOnline(): boolean {
    return isOnline();
  }

  public get focusChanged() : Observable<FocusChangedEvent> {
    return merge(
      fromEvent(window, 'visibilitychange').pipe(map(() => ({ isFocused: isFocused() }))),
      fromEvent(window, 'focus').pipe(map(() => ({ isFocused: isFocused() }))),
    );
  }

  public get isFocused(): boolean {
    return isFocused();
  }
}
