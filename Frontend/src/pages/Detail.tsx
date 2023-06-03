import React from "react";
import { useParams } from "react-router-dom";
import TimeCapsuleService from "../services/TimeCapsuleService";
import ITimeCapsuleModel from "../types/TimeCapsuleModel";
import EditTimeCapsule from "../components/TimeCapsule/EditTimeCapsule";
import AddTimeCapsuleItem from "../components/TimeCapsuleItem/AddTimeCapsuleItem";
import DeleteTimeCapsuleItem from "../components/TimeCapsuleItem/DeleteTimeCapsuleItem";
import EditTimeCapsuleItem from "../components/TimeCapsuleItem/EditTimeCapsuleItem";
import GetTimeCapsuleItem from "../components/TimeCapsuleItem/GetTimeCapsuleItem";

function Detail() {
  const { id } = useParams<{ id: string }>();
  const [TimeCapsule, SetTimeCapsule] = React.useState<ITimeCapsuleModel>();
  const timeCapsuleId = Number(id);
  return (
    <>
      <EditTimeCapsule timeCapsuleId={timeCapsuleId}></EditTimeCapsule>
      <div className="m-4">
        <AddTimeCapsuleItem
          timeCapsuleId={timeCapsuleId.toString()}
        ></AddTimeCapsuleItem>
        <GetTimeCapsuleItem
          timeCapsuleId={timeCapsuleId.toString()}
        ></GetTimeCapsuleItem>
        <DeleteTimeCapsuleItem></DeleteTimeCapsuleItem>
      </div>
    </>
  );
}

export default Detail;
