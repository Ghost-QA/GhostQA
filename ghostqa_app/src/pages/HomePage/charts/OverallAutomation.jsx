import React from 'react';
import ReactApexChart from 'react-apexcharts';

const OverallAutomation = ({ data }) => {
  const options = {
    chart: {
      width: 380,
      type: 'pie',
    },
    labels: ['Automated', 'Not Automated'],
    responsive: [
      {
        breakpoint: 480,
        options: {
          chart: {
            width: 200,
          },
          legend: {
            position: 'bottom',
          },
        },
      },
    ],
  };

  const series = [data?.perAutomatedTestcases, data?.perNotAutomatedTestcases];

  return (
    <div id="chart">
      <ReactApexChart options={options} series={series} type="pie" width={380} />
    </div>
  );
};

export default OverallAutomation;
