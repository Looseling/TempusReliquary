import React from "react";
import AddTimeCapsule from "../components/TimeCapsule/AddTimeCapsule";
import TimeCapsuleList from "../components/TimeCapsule/TimeCapsuleList";
import { Box, Container } from "@mui/material";

function TimeCapsule() {
  return (
    <>
      <AddTimeCapsule></AddTimeCapsule>
      <h1>Collection</h1>
      <TimeCapsuleList viewMode="user"></TimeCapsuleList>
      <h1>12312</h1>
    </>
  );
}

export default TimeCapsule;
