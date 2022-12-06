
import { observer } from 'mobx-react-lite';
import React from 'react';
import { Link } from 'react-router-dom';
import { Image, List } from 'semantic-ui-react';
import { Profile } from '../../../app/models/profile';

interface Props {
    attendess: Profile[];
}

export default observer(function ActivityListItemAttendee({ attendess }: Props) {

    return (
        
        <List horizontal>
            {attendess.map(attendee => (
                <List.Item key={attendee.username} as={Link} to={`/profiles/${attendee.username}`}>
                    <Image size='mini' circular src={attendee.image || '/assets/user.png'} />
                </List.Item>
            ))}

        </List>

    )
}) 