import { QueryOptions } from './../../../../node_modules/@testing-library/dom/types/suggestions.d';
import { of } from 'rxjs';
import { initialQueryState, Query } from './query.state';
import { QueryKey, QueryFunction } from './types';
import { buildQuery, ensureQueryKeyArray, hashQueryKey, isActive, isPlainObject, isQueryKey, isStale, isStaleByTime, timeUntilStale } from './utils';



describe('utils', () => {
  const realDateNow = Date.now.bind(global.Date);
  const now = 1487076708000;

  const createDummyQuery = (): Query => {
    return {
      queryKey: 'any query key',
      queryHash: 'any query hash',
      queryFn: () => of({}),
      options: {
        refetchInterval: 0,
        refetchIntervalInBackground: false,
        refetchOnWindowFocus: true,
        refetchOnReconnect: true,
        staleTime: 0,
        cacheTime: 30000,
      },
      state: {
        data: undefined,
        dataUpdatedAt: 0,
        error: null,
        errorUpdatedAt: 0,
        fetchFailureCount: 0,
        refCount: 0,
        isFetching: false,
        isInvalidated: true,
        isPaused: false,
        status: 'idle',
      },
    };
  };

  beforeEach(() => {
    global.Date.now = jest.fn(() => now);
  });

  afterEach(() => {
    global.Date.now = realDateNow;
  })

  describe('isPlainObject', () => {
    it('should return `true` for a plain object', () => {
      expect(isPlainObject({})).toEqual(true);
    });

    it('should return `false` for an array', () => {
      expect(isPlainObject([])).toEqual(false);
    });

    it('should return `false` for null', () => {
      expect(isPlainObject(null)).toEqual(false);
    });

    it('should return `false` for undefined', () => {
      expect(isPlainObject(undefined)).toEqual(false);
    });

    it('should return `true` for object with an undefined constructor', () => {
      expect(isPlainObject(Object.create(null))).toBeTruthy();
    });

    it('should return `false` if constructor does not have an Object-specific method', () => {
      class Foo {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        abc: any;
        constructor() {
          this.abc = {};
        }
      };

      expect(isPlainObject(new Foo())).toBeFalsy();
    })

    it('should return `false` if the object has a modified prototype', () => {
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      function Graph(this: any) {
        this.vertices = []
        this.edges = []
      }

      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      Graph.prototype.addVertex = function (v: any) {
        this.vertices.push(v)
      }

      expect(isPlainObject(Object.create(Graph))).toBeFalsy()
    })
  });

  describe('isQueryKey', () => {
    it('should return `true` if the input is a string', () => {
      expect(isQueryKey('any string')).toBeTruthy();
    });

    it('should return `true` if the input is an array', () => {
      expect(isQueryKey(['any string', 2, {}])).toBeTruthy();
    });

    it('should return `false` if the input is an object', () => {
      expect(isQueryKey({})).toBeFalsy();
    });

    it('should return `false` if the input is null', () => {
      expect(isQueryKey(null)).toBeFalsy();
    });

    it('should return `false` if the input is undefined', () => {
      expect(isQueryKey(null)).toBeFalsy();
    });
  });

  describe('ensureQueryKeyArray', () => {
    it('should return an query key array if the input is a string', () => {
      const actual = ensureQueryKeyArray('any string key');
      expect(actual).toStrictEqual(['any string key']);
    });

    it('should return the input key if the input is an array', () => {
      const queryKey: QueryKey = ['any string', 2, {}];
      const actual = ensureQueryKeyArray(queryKey);
      expect(actual).toBe(queryKey);
    });
  });

  describe('hashQueryKey', () => {
    it('should return the jsonified query key array if it is a string', () => {
      const actual = hashQueryKey('any string');
      expect(actual).toBe('["any string"]');
    });

    it('should return the jsonified query key array if it is an array ', () => {
      const actual = hashQueryKey(['any string', 2, {}]);
      expect(actual).toBe('["any string",2,{}]');
    });

    it('should return a query key array sequence independant hash', () => {
      const actual = hashQueryKey(['c', 'b', 'a']);
      expect(actual).toBe('["c","b","a"]');
    });
  });

  describe('timeUntilStale', () => {
    it('should return the remaining time if updatedAt + staleTime is greater than now', () => {
      const updatedAt = now - 10;
      const staleTime = 15;

      const actual = timeUntilStale(updatedAt, staleTime);

      expect(actual).toBe(5);
    });

    it('should return `0` if updatedAt + staleTime is smaller than now', () => {
      const updatedAt = now - 10;
      const staleTime = 5;

      const actual = timeUntilStale(updatedAt, staleTime);

      expect(actual).toBe(0);
    });

    it('should return use a default staleTime of 0 if non get sprovided', () => {
      const updatedAt = now - 1;

      const actual = timeUntilStale(updatedAt);

      expect(actual).toBe(0);
    });
  });

  describe('isStale', () => {
    it('should return `false` if the query has been updated', () => {
      const query: Query = {
        ...createDummyQuery(),
        state: {
          ...createDummyQuery().state,
          isInvalidated: false,
          dataUpdatedAt: now - 10,
        },
      };

      const actual = isStale(query);

      expect (actual).toBeFalsy();
    });

    it('should return `true` if the query has been invalidated', () => {
      const query: Query = {
        ...createDummyQuery(),
        state: {
          ...createDummyQuery().state,
          isInvalidated: true,
          dataUpdatedAt: now - 10,
        },
      };

      const actual = isStale(query);

      expect (actual).toBeTruthy();
    });

    it('should return `true` if the query never has been updated', () => {
      const query: Query = {
        ...createDummyQuery(),
        state: {
          ...createDummyQuery().state,
          isInvalidated: false,
          dataUpdatedAt: 0,
        },
      };

      const actual = isStale(query);

      expect (actual).toBeTruthy();
    });
  });

  describe('isStaleByTime', () => {
    it('should return `false` if the query has been updated within the state time', () => {
      const query: Query = {
        ...createDummyQuery(),
        state: {
          ...createDummyQuery().state,
          isInvalidated: false,
          dataUpdatedAt: now - 10,
        },
      };

      const actual = isStaleByTime(query, 15);

      expect (actual).toBeFalsy();
    });

    it('should return `true` if the query has been invalidated', () => {
      const query: Query = {
        ...createDummyQuery(),
        state: {
          ...createDummyQuery().state,
          isInvalidated: true,
          dataUpdatedAt: now - 10,
        },
      };

      const actual = isStaleByTime(query, 15);

      expect (actual).toBeTruthy();
    });

    it('should return `true` if the query never has been updated', () => {
      const query: Query = {
        ...createDummyQuery(),
        state: {
          ...createDummyQuery().state,
          isInvalidated: false,
          dataUpdatedAt: 0,
        },
      };

      const actual = isStaleByTime(query, 15);

      expect (actual).toBeTruthy();
    });

    it('should return `true` if the query has passt the stale time', () => {
      const query: Query = {
        ...createDummyQuery(),
        state: {
          ...createDummyQuery().state,
          isInvalidated: false,
          dataUpdatedAt: now - 10,
        },
      };

      const actual = isStaleByTime(query, 5);

      expect (actual).toBeTruthy();
    });
  });

  describe('isActive', () => {
    it('should return `true` if the query has references and isPaused is `false`', () => {
      const query = {
        ...createDummyQuery(),
        state: {
          ...createDummyQuery().state,
          refCount: 1,
          isPaused: false,
        }
      }

      const actual = isActive(query);

      expect(actual).toBeTruthy();
    });

    it('should return `false` if isPaused is `true`', () => {
      const query = {
        ...createDummyQuery(),
        state: {
          ...createDummyQuery().state,
          refCount: 1,
          isPaused: true,
        }
      }

      const actual = isActive(query);

      expect(actual).toBeFalsy();
    });

    it('should return `false` if refCount is `0`', () => {
      const query = {
        ...createDummyQuery(),
        state: {
          ...createDummyQuery().state,
          refCount: 0,
          isPaused: false,
        },
      };

      const actual = isActive(query);

      expect(actual).toBeFalsy();
    });
  });

  describe('buildQuery', () => {
    it('should return a query instance with the provided queryKey and queryFn', () => {
      const state = {
        ...initialQueryState,
      };

      const queryKey: QueryKey = 'any query key';
      const queryFn: QueryFunction = () => of({});
      const queryObt: QueryOptions = {};

      const actual = buildQuery(state, queryKey, queryFn, queryObt);

      expect(actual.queryKey).toStrictEqual(queryKey);
      expect(actual.queryHash).toStrictEqual('["any query key"]');
      expect(actual.queryFn).toBe(queryFn);
    });
  });
});
