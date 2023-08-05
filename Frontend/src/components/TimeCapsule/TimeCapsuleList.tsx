import React, { useEffect, useState } from "react";
import ITimeCapsuleModel from "../../types/TimeCapsuleModel";
import TimeCapsuleService from "../../services/TimeCapsuleService";
import { Card, Col, Row, Table } from "react-bootstrap";
import TimeCapsuleListItem from "./TimeCapsuleListItem";
import { Link } from "react-router-dom";
import PreviewTimeCapsule from "../../pages/ViewTimeCapsule";

type TimeCapsuleListProps = {
  viewMode: string;
};

function TimeCapsuleList({ viewMode }: TimeCapsuleListProps) {
  const [TimeCapsules, SetTimeCapsules] = useState<ITimeCapsuleModel[]>();
  const [AddTimeCapsule, SetAddTimeCapsule] = useState<boolean>(false);
  useEffect(() => {
    retrieveTimeCapsule();
  }, []);

  const retrieveTimeCapsule = () => {
    if (viewMode == "user") {
      TimeCapsuleService.getAll()
        .then((response) => {
          SetTimeCapsules(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    } else if (viewMode == "public") {
      TimeCapsuleService.getAllUploaded()
        .then((response) => {
          SetTimeCapsules(response.data);
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    }
  };

  // const refreshList = () => {
  //   retrieveTimeCapsule();
  // };
  return (
    <>
      <Row md={3} xs={1} lg={4} className="g-3">
        {TimeCapsules?.map((TimeCapsule) => (
          <Col key={TimeCapsule.id}>
            <div>
              {viewMode == "user" && (
                <TimeCapsuleListItem timeCapsule={TimeCapsule} />
              )}
              {viewMode == "public" && (
                <TimeCapsuleListItem timeCapsule={TimeCapsule} />
              )}
            </div>
          </Col>
        ))}
      </Row>
    </>
  );
}

export default TimeCapsuleList;
