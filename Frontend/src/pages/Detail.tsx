import React from "react";
import { useParams } from "react-router-dom";
import TimeCapsuleService from "../services/TimeCapsuleService";
import ITimeCapsuleModel from "../types/TimeCapsuleModel";
import EditTimeCapsule from "../components/EditTimeCapsule";

function Detail() {
  const { id } = useParams<{ id: string }>();
  const [TimeCapsule, SetTimeCapsule] = React.useState<ITimeCapsuleModel>();
  const timeCapsuleId = Number(id);
  return (
    <>
      <EditTimeCapsule timeCapsuleId={timeCapsuleId}></EditTimeCapsule>
    </>
  );
}

export default Detail;
