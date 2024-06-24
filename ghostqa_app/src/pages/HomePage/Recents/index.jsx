import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import CustomStatusCell from "./CustomStatusCell";
import { Link } from "react-router-dom";
import { useStyles, StyledTableCell } from "./style";

const RecentsTable = ({ data }) => {
  const classes = useStyles();
  const [activeRow, setActiveRow] = React.useState(null);

  const handleRowClick = (payload) => {
  
    setActiveRow((prevSuite) => (prevSuite === payload ? null : payload));
  };

  return (
    <TableContainer>
      <Table>
        <TableHead>
          <TableRow>
            <StyledTableCell>Suite Name</StyledTableCell>
            <StyledTableCell>Run By</StyledTableCell>
            <StyledTableCell>Last Executed</StyledTableCell>
            <StyledTableCell >Status</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data?.map((row) => (
            <TableRow
              key={row.TestSuiteName}
              className={`${classes.tableRow} ${
                row === activeRow ? classes.activeRow : ""
              }`}
            >
              <StyledTableCell component="th" scope="row">
                <Link
                to={`/local-testing/${row?.TestSuiteName}`}
                  style={{ textDecoration: "none", cursor: "pointer" }}
                  onClick={() => handleRowClick(row)}
                >
                  {row.TestSuiteName}
                </Link>
              </StyledTableCell>
              <StyledTableCell style={{marginLeft:'10px'}}>
                {row.TesterName}
              </StyledTableCell>
              <StyledTableCell >
                {row.TestSuiteEndDate}
              </StyledTableCell>
              <StyledTableCell>
                <CustomStatusCell status={row.TestCaseStatus} />
              </StyledTableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default RecentsTable;
