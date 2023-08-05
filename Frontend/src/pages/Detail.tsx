import React from "react";
import { useParams } from "react-router-dom";
import TimeCapsuleService from "../services/TimeCapsuleService";
import ITimeCapsuleModel from "../types/TimeCapsuleModel";
import EditTimeCapsule from "../components/TimeCapsule/EditTimeCapsule";
import AddTimeCapsuleItem from "../components/TimeCapsuleItem/AddTimeCapsuleItem";
import DeleteTimeCapsuleItem from "../components/TimeCapsuleItem/DeleteTimeCapsuleItem";
import EditTimeCapsuleItem from "../components/TimeCapsuleItem/EditTimeCapsuleItem";
import GetTimeCapsuleItem from "../components/TimeCapsuleItem/GetTimeCapsuleItem";
import PreviewTimeCapsule from "./ViewTimeCapsule";
import AddTimeCapsuleReceivers from "../components/TimeCapsuleItem/AddTimeCapsuleReceivers";

function Detail() {
  const { id } = useParams<{ id: string }>();
  const [TimeCapsule, SetTimeCapsule] = React.useState<ITimeCapsuleModel>();
  const timeCapsuleId = Number(id);
  return (
    <>
      <div className="m-4 d-flex justify-content-between">
        <div style={{ width: "70%" }}>
          <EditTimeCapsule timeCapsuleId={timeCapsuleId}></EditTimeCapsule>
          <PreviewTimeCapsule></PreviewTimeCapsule>
        </div>
        <div style={{ width: "30%" }}>
          <AddTimeCapsuleItem
            timeCapsuleId={timeCapsuleId.toString()}
          ></AddTimeCapsuleItem>
          <AddTimeCapsuleReceivers
            timeCapsuleId={timeCapsuleId.toString()}
          ></AddTimeCapsuleReceivers>
        </div>
      </div>
    </>
  );
}

export default Detail;
