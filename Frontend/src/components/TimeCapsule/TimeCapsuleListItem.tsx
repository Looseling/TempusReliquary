import { Button, Card } from "react-bootstrap";
import ITimeCapsuleModel from "../../types/TimeCapsuleModel";
import "../../css/globalStyles.css";
import { Link } from "react-router-dom";

type ListItemProps = {
  timeCapsule: ITimeCapsuleModel;
};

function TimeCapsuleListItem({ timeCapsule }: ListItemProps) {
  const cardColor = timeCapsule.isUploaded
    ? "uploaded-card"
    : "not-uploaded-card";
  const redirectTo = timeCapsule.isUploaded
    ? `/ViewTimeCapsule/${timeCapsule.id}`
    : `/TimeCapsule/detail/${timeCapsule.id}`;

  const redirectButtonText = timeCapsule.isUploaded ? "View" : "Edit";
  const redirectButtonColor = timeCapsule.isUploaded ? "primary" : "success";

  const date = new Date(timeCapsule.openingDate);
  const formattedDate = `${date.getDate()}/${
    date.getMonth() + 1
  }/${date.getFullYear()}`;

  return (
    <Card style={{ width: "18rem" }} className="disabled">
      <Card.Img variant="top" src={"images/DefaultCapsule.jpg"}></Card.Img>
      <Card.Body className={`${cardColor}`}>
        <Card.Title className={`${cardColor}`}>{timeCapsule.title}</Card.Title>
        <Card.Text className={`${cardColor}`}>{formattedDate}</Card.Text>
        <Link to={`${redirectTo}`}>
          <Button variant={redirectButtonColor}>{redirectButtonText}</Button>
        </Link>
      </Card.Body>
    </Card>
  );
}

export default TimeCapsuleListItem;
