import React, { useEffect } from "react";
import ITimeCapsuleModel from "../types/TimeCapsuleModel";
import GetTimeCapsuleItem from "../components/TimeCapsuleItem/GetTimeCapsuleItem";
import TimeCapsuleService from "../services/TimeCapsuleService";
import { useParams } from "react-router-dom";
import "../css/globalStyles.css";

function PreviewTimeCapsule() {
  const { id } = useParams<{ id: string }>();
  const [timeCapsule, setTimeCapsule] = React.useState<ITimeCapsuleModel>();
  const timeCapsuleId = Number(id);

  useEffect(() => {
    retrieveTimeCapsule();
  }, []);

  const retrieveTimeCapsule = () => {
    TimeCapsuleService.getById(timeCapsuleId)
      .then((response) => {
        setTimeCapsule(response.data);
        console.log(response.data);
      })
      .catch((e) => {
        console.log(e);
      });
  };

  return (
    <div className="time-capsule-container">
      <div className="time-capsule-card">
        <h1>{timeCapsule?.title}</h1>
        <h2 className="time-capsule-subtitle">Time Capsule Description</h2>
        <p>{timeCapsule?.description}</p>
        <GetTimeCapsuleItem timeCapsuleId={timeCapsuleId.toString()} />
      </div>
    </div>
  );
}

export default PreviewTimeCapsule;
