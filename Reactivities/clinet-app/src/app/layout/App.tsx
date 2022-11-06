import React, { Fragment, useEffect, useState } from "react";
import axios from "axios";
import { Container } from "semantic-ui-react";
import { Activity } from "../models/activity";
import NavBar from "./NavBar";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]); //we use interface from app//layout => type safety
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>( undefined );
  const [editMode, SetEditMode] = useState(false);

  useEffect(() => {
    axios
      .get<Activity[]>("http://localhost:5000/api/activities")
      .then((response) => {
        setActivities(response.data);
      });
  }, []);

  function handleSelectActivity(id: string) {
    setSelectedActivity(activities.find((x) => x.id=== id));
  }

  function handleCancelSelectedActivity() {
    setSelectedActivity(undefined);
  }

  function handleFormOpen(id?: string) 
  {
    id ? handleSelectActivity(id):handleCancelSelectedActivity()
    SetEditMode(true);
  }

  //Fragment == <>
  return (
    <>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboard 
        activities={activities} 
        selectedActivity= {selectedActivity}
        selectActivity ={handleSelectActivity}
        cancelSelctedActivity={handleCancelSelectedActivity}
        
        />
      </Container>
    </>
  );
}

export default App;
