import React from "react";
import { Grid } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import ActivityList from "./ActivityList";

interface Props {
  activities: Activity[]; //list of activietes
  selectedActivity: Activity | undefined;
  selectActivity: (id: string) => void;
  cancelSelctedActivity: () => void;
}
//distruct by props
export default function ActivityDashboard({
  activities,
  selectedActivity,
  selectActivity,
  cancelSelctedActivity,
}: Props) {
  return (
    <Grid>
      <Grid.Column width="10">
        <ActivityList activities={activities} selectActivity={selectActivity} />
      </Grid.Column>
      <Grid.Column width="6">
        {selectedActivity &&
         <ActivityDetails activity={selectedActivity}  cancelSelctedActivity={cancelSelctedActivity} />}
        <ActivityForm />
      </Grid.Column>
    </Grid>
  );
}
