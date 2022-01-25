/** @jsxImportSource @emotion/react */
import { css, keyframes } from '@emotion/react';
import * as React from 'react';

import logo from './logo.svg';

const appLogoSpin = keyframes({
  from: {
    transform: 'rotate(0deg)',
  },
  to: {
    transform: 'rotate(360deg)',
  },
});

function App() {
  return (
    <div css={css({ textAlign: 'center' })}>
      <header
        css={css({
          backgroundColor: '#282c34',
          minHeight: '100vh',
          display: 'flex',
          flexdirection: 'column',
          alignItems: 'center',
          justifyContent: 'center',
          fontSize: 'calc(10px + 2vmin)',
          color: 'white',
        })}
      >
        <img
          src={logo}
          css={css({
            height: '40vmin',
            pointerEvents: 'none',
            '@media (prefers-reduced-motion: no-preference)': {
              animation: `${appLogoSpin} infinite 20s linear`,
            },
          })}
          alt="logo"
        />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a css={css({ color: '#61dafb' })} href="https://reactjs.org" target="_blank" rel="noopener noreferrer">
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
