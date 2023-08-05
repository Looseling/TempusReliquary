import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Form, Button } from "react-bootstrap";
import TimeCapsuleService from "../../services/TimeCapsuleService";
import TimeCapsule from "../../pages/TimeCapsule";

type EditTimeCapsuleProps = {
  timeCapsuleId: number;
};

const EditTimeCapsule = ({ timeCapsuleId }: EditTimeCapsuleProps) => {
  const [timeCapsule, setTimeCapsule] = useState({
    id: timeCapsuleId,
    title: "",
    description: "",
    openingDate: "",
  });

  useEffect(() => {
    retrieveTimeCapsule();
  }, []);

  const navigate = useNavigate();

  // Simulating fetching the time capsule data based on the provided ID
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

  const handleInputChange = (e: any) => {
    const { name, value } = e.target;
    setTimeCapsule((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSubmit = (e: any) => {
    TimeCapsuleService.update(timeCapsuleId, timeCapsule).catch((e) => {
      console.log(e);
    });
  };

  const handleDelete = (e: any) => {
    e.preventDefault();
    TimeCapsuleService.remove(timeCapsuleId).then(() => {
      navigate("/timecapsule");
    });
  };

  const handleUpload = (e: any) => {
    e.preventDefault();
    TimeCapsuleService.upload(timeCapsuleId, timeCapsule).then(() => {
      navigate("/timecapsule");
    });
  };

  const today = new Date();
  const yyyy = today.getFullYear();
  const mm = String(today.getMonth() + 1).padStart(2, "0"); // Months are zero indexed, so +1
  const dd = String(today.getDate()).padStart(2, "0");

  const minDate = `${yyyy}-${mm}-${dd}`;

  return (
    <div>
      <h2>Edit Time Capsule</h2>
      <div>
        <Form onSubmit={handleSubmit}>
          <Form.Group controlId="title">
            <Form.Label>Title</Form.Label>
            <Form.Control
              type="text"
              required
              value={timeCapsule.title}
              onChange={handleInputChange}
              name="title"
            />
          </Form.Group>

          <Form.Group controlId="description">
            <Form.Label>Message to the future...</Form.Label>
            <Form.Control
              type="text"
              required
              value={timeCapsule.description}
              onChange={handleInputChange}
              name="description"
              as="textarea"
              rows={10}
            />
          </Form.Group>

          <Form.Group controlId="openingDate">
            <Form.Label>Opening Date</Form.Label>
            <Form.Control
              type="date"
              required
              value={timeCapsule.openingDate}
              onChange={handleInputChange}
              name="openingDate"
              min={minDate}
            />
          </Form.Group>

          <Button variant="primary" type="submit">
            Save
          </Button>
          <Button
            className="mx-2"
            variant="danger"
            type="button"
            onClick={handleDelete}
          >
            Delete
          </Button>

          <Button
            className="mx-2"
            variant="success"
            type="button"
            onClick={handleUpload}
          >
            Upload
          </Button>
        </Form>
      </div>
    </div>
  );
};

export default EditTimeCapsule;
