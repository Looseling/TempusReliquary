import { Button, Card } from "react-bootstrap";
import ITimeCapsuleModel from "../../types/TimeCapsuleModel";

type ListItemProps = {
  timeCapsule: ITimeCapsuleModel;
};

function TimeCapsuleListItem({ timeCapsule }: ListItemProps) {
  return (
    <Card className="h-100">
      <Card.Img
        variant="top"
        src={"images/DefaultCapsule.jpg"}
        height="200px"
        style={{ objectFit: "cover" }}
      ></Card.Img>
      <Card.Body className="d-flex flex-column">
        <Card.Title className="d-flex justify-content-between align-items-baseline mb-4">
          <span className="fs-2">{timeCapsule.title}</span>
          <span className="ms-2 text-muted">{timeCapsule.openingDate}</span>
        </Card.Title>
      </Card.Body>
    </Card>
  );
}

export default TimeCapsuleListItem;
