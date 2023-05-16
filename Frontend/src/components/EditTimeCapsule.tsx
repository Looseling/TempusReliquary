import React, { useState, useEffect } from "react";
import { Form, Button } from "react-bootstrap";
import TimeCapsuleService from "../services/TimeCapsuleService";
import TimeCapsule from "../pages/TimeCapsule";

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

  return (
    <div>
      <h2>Edit Time Capsule</h2>
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
          <Form.Label>Description</Form.Label>
          <Form.Control
            type="text"
            required
            value={timeCapsule.description}
            onChange={handleInputChange}
            name="description"
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
          />
        </Form.Group>

        <Button variant="primary" type="submit">
          Save Changes
        </Button>
      </Form>
    </div>
  );
};

export default EditTimeCapsule;
