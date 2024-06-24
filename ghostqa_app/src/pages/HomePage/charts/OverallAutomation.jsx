import React from 'react';
import ReactApexChart from 'react-apexcharts';

const OverallAutomation = ({ data }) => {
  const options = {
    chart: {
      width: 380,
      type: 'pie',
      toolbar: {
        show: false
      },
    },
    labels: ['Automated', 'Not Automated'],
    dataLabels: {
      enabled: true,
      formatter: function (val, opts) {
        const total = opts.w.globals.series.reduce((a, b) => a + b, 0);
        const percentage = ((val / total) * 100).toFixed(1) + "%";
        return percentage;
      },
      offsetX: -10,
      offsetY: -10,
      style: {
        colors: ["#fff"],
        fontSize: "12px",
        fontFamily: "Helvetica, Arial, sans-serif",
        fontWeight: "600",
      },
      background: {
        enabled: true,
        foreColor: "#000",
        borderWidth: 1,
        borderColor: "#ccc",
        opacity: 0.85,
        padding: 4, 
        dropShadow: {
          enabled: false,
        },
      },
    },
    plotOptions: {
      pie: {
        dataLabels: {
          offset: -20,
          minAngleToShowLabel: 10,
        }
      }
    },
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

