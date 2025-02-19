import React, { useMemo } from 'react';

const LoadingBar: React.FC = () => {
  const keyframesStyle = useMemo(() => `
    @keyframes loading {
      0% {
        transform: translateX(-100%);
      }
      100% {
        transform: translateX(300%);
      }
    }
  `, []);

  const containerStyle: React.CSSProperties = useMemo(() => ({
    width: '100%',
    height: '5px',
    backgroundColor: '#e0e0e0',
    borderRadius: '5px',
    overflow: 'hidden',
    position: 'relative'
  }), []);

  const barStyle: React.CSSProperties = useMemo(() => ({
    position: 'absolute',
    top: 0,
    left: 0,
    width: '50%',
    height: '100%',
    backgroundColor: '#3b82f6',
    animation: 'loading 1.5s infinite linear'
  }), []);

  return (
    <>
      <style>{keyframesStyle}</style>
      <div style={containerStyle}>
        <div style={barStyle}></div>
      </div>
    </>
  );
}

export default React.memo(LoadingBar);