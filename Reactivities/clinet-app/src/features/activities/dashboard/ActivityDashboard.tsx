import React from "react";
import { Grid } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import ActivityList from "./ActivityList";

interface Props {
  activities: Activity[]; //list of activietes
  selectedActivity: Activity | undefined; //the activity that we have in "hand"
  selectActivity: (id: string) => void;
  cancelSelctedActivity: () => void;
  editMode: boolean;
  openForm: (id: string) => void;
  closeForm: () => void;
  createOrEdit : (activity: Activity)=> void;
  deleteActivity : (id : string) => void
}

//distruct by props
export default function ActivityDashboard({
  activities,
  selectedActivity,
  selectActivity,
  cancelSelctedActivity, editMode, openForm, closeForm,createOrEdit,deleteActivity
}: Props) {
  return (
    <Grid>
      <Grid.Column width="10">
        <ActivityList activities={activities} selectActivity={selectActivity} deleteActivity={deleteActivity} />
      </Grid.Column>
      <Grid.Column width="6">
        {selectedActivity && !editMode&&
          <ActivityDetails activity={selectedActivity}
            cancelSelctedActivity={cancelSelctedActivity} openForm={openForm} />
        }
        {editMode && <ActivityForm closeForm={closeForm} activity={selectedActivity} createOrEdit= {createOrEdit} />}
      </Grid.Column>
    </Grid>
  );
}
