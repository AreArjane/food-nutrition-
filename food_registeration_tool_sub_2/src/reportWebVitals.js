// Define the reportWebVitals function, which accepts an optional callback parameter (onPerfEntry)
const reportWebVitals = onPerfEntry => {
  
  // Check if onPerfEntry is provided and is a function
  if (onPerfEntry && onPerfEntry instanceof Function) {
    import('web-vitals').then(({ getCLS, getFID, getFCP, getLCP, getTTFB }) => {
      
      // Call each of the web-vitals functions and pass the callback to collect metrics
      getCLS(onPerfEntry); // Cumulative Layout Shift (CLS) - measures visual stability
      getFID(onPerfEntry); // First Input Delay (FID) - measures input responsiveness
      getFCP(onPerfEntry); // First Contentful Paint (FCP) - measures time to first visible content
      getLCP(onPerfEntry); // Largest Contentful Paint (LCP) - measures time to largest visible content
      getTTFB(onPerfEntry); // Time to First Byte (TTFB) - measures server response time
    });
  }
};

// Export the reportWebVitals function as the default export
export default reportWebVitals;
