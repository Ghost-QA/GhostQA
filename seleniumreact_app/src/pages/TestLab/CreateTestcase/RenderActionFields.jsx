import {
  FormControl,
  FormControlLabel,
  Grid,
  Radio,
  RadioGroup,
} from "@mui/material";
import React, { useState } from "react";
import {
  StyledFormControl,
  StyledOutlinedInput,
  StyledTextField,
} from "./styleTestCase";
import { keyList, accessibilityList, users } from "../DropDownOptions";
import Select from "react-select";
import { testCases } from "../DropDownOptions";
export default function RenderActionFields({
  action,
  step,
  index,
  Errors,
  handleInputChange,
  isEditable,
}) {
  switch (action) {
    case "type":
      return (
        <>
          <Grid item xs={6}>
            <StyledFormControl>
              <StyledOutlinedInput
                type="text"
                placeholder="Input value"
                disabled={!isEditable}
                error={Errors[index]?.sendKeyInputError}
                value={step?.sendKeyInput}
                onChange={(e) => {
                  handleInputChange(e, index, "sendKeyInput");
                }}
              />
            </StyledFormControl>
          </Grid>
        </>
      );
    case "scroll_to_window":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="Window pixel"
              disabled={!isEditable}
              error={Errors[index]?.scrollPixelError}
              value={step?.scrollPixel}
              onChange={(e) => {
                handleInputChange(e, index, "scrollPixel");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "go_to_url":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="URL"
              disabled={!isEditable}
              error={Errors[index]?.urlError}
              value={step?.url}
              onChange={(e) => {
                handleInputChange(e, index, "url");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "select_option":
      return (
        <Grid item xs={6}>
           <Select
              isClearable={true}
              placeholder="select user"
              options={users}
              value={
                step
                  ? step.selectedUser
                    ? {
                        label: step.selectedUser,
                        value: step.selectedUser,
                      }
                    : null
                  : null
              }
              onChange={(act) => handleInputChange(act, index, "selectedUser")}
              styles={{
                container: (provided) => ({
                  ...provided,
                  backgroundColor: "rgb(242, 242, 242)",
                  width: "100%",
                }),
                control: (provided, state) => ({
                  ...provided,
                  backgroundColor: "rgb(242, 242, 242)",
                  "&:hover": {
                    borderColor: "#654DF7",
                  },
                  borderColor: Errors[index]?.selectedUserError
                    ? "red"
                    : state.isFocused
                    ? "#654DF7"
                    : "rgb(242, 242, 242)",
                }),
                option: (provided, state) => ({
                  ...provided,
                  backgroundColor: state.isSelected ? "#654DF7" : "transparent",
                }),
                clearIndicator: (provided) => ({
                  ...provided,
                  cursor: "pointer",
                  ":hover": {
                    color: "#654DF7",
                  },
                }),
                dropdownIndicator: (provided) => ({
                  ...provided,
                  cursor: "pointer",
                  ":hover": {
                    color: "#654DF7",
                  },
                }),
              }}
              menuPosition={"fixed"}
            />
        </Grid>
      )
    case "upload_file":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="file"
              placeholder="File here"
              disabled={!isEditable}
              error={Errors[index]?.fileNameError}
              // value={step?.fileName}  not aplicable for file
              onChange={(e) => {
                handleInputChange(e, index, "fileName");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "element_has_value":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="Input value"
              disabled={!isEditable}
              value={step?.elementValue}
              error={Errors[index]?.elementValueError}
              onChange={(e) => {
                handleInputChange(e, index, "elementValue");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "element_has_css_property_with_value":
      return (
        <>
          <Grid item xs={6}>
            <StyledFormControl>
              <StyledOutlinedInput
                type="text"
                placeholder="css property"
                disabled={!isEditable}
                value={step?.cssProperty}
                error={Errors[index]?.cssPropertyError}
                onChange={(e) => {
                  handleInputChange(e, index, "cssProperty");
                }}
              />
            </StyledFormControl>
          </Grid>
          <Grid item xs={6}>
            <StyledFormControl>
              <StyledOutlinedInput
                type="text"
                placeholder="css value"
                disabled={!isEditable}
                value={step?.cssValue}
                error={Errors[index]?.cssValueError}
                onChange={(e) => {
                  handleInputChange(e, index, "cssValue");
                }}
              />
            </StyledFormControl>
          </Grid>
        </>
      );
    case "validate_page_title":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="Page title"
              disabled={!isEditable}
              error={Errors[index]?.pageTitleError}
              value={step?.pageTitle}
              onChange={(e) => {
                handleInputChange(e, index, "pageTitle");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "validate_current_url":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="Current url"
              value={step?.currentUrl}
              disabled={!isEditable}
              error={Errors[index]?.currentUrlError}
              onChange={(e) => {
                handleInputChange(e, index, "currentUrl");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "should_not_equal":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="input value"
              disabled={!isEditable}
              error={Errors[index]?.shouldNotEqualError}
              value={step?.shouldNotEqualValue}
              onChange={(e) => {
                handleInputChange(e, index, "shouldNotEqualValue");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "should_include":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="input value"
              disabled={!isEditable}
              error={Errors[index]?.shouldIncludeError}
              value={step?.shouldIncludeValue}
              onChange={(e) => {
                handleInputChange(e, index, "shouldIncludeValue");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "should_equal":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="input value"
              disabled={!isEditable}
              error={Errors[index]?.shouldEqualError}
              value={step?.shouldEqualValue}
              onChange={(e) => {
                handleInputChange(e, index, "shouldEqualValue");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "should_be_greater_than":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="input value"
              disabled={!isEditable}
              error={Errors[index]?.shouldGreaterThanError}
              value={step?.shouldGreaterThanValue}
              onChange={(e) => {
                handleInputChange(e, index, "shouldGreaterThanValue");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "should_be_less_than":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="input value"
              disabled={!isEditable}
              error={Errors[index]?.shouldLessError}
              value={step?.shouldLessValue}
              onChange={(e) => {
                handleInputChange(e, index, "shouldLessValue");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "contain_text":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="input value"
              disabled={!isEditable}
              error={Errors[index]?.containTextError}
              value={step?.containTextValue}
              onChange={(e) => {
                handleInputChange(e, index, "containTextValue");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    case "have_attribute":
      return (
        <Grid item xs={6}>
          <StyledFormControl>
            <StyledOutlinedInput
              type="text"
              placeholder="input value"
              disabled={!isEditable}
              error={Errors[index]?.haveAttributeError}
              value={step?.haveAttributeValue}
              onChange={(e) => {
                handleInputChange(e, index, "haveAttributeValue");
              }}
            />
          </StyledFormControl>
        </Grid>
      );
    default:
      return null;
      








  }
}
