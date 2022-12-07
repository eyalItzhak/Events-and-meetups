
import React, { useEffect, useState } from 'react';
import { Button, Grid, Header} from 'semantic-ui-react';
import PhotoWidgetCropper from './PhotoWidgetCropper';
import PhotoWidgetDropzone from './PhotoWidgetDropzone';
// import { Cropper } from "react-cropper";


interface Props {
    loading : boolean;
    uploadPhoto : (file:Blob) => void;
}

export default function PhotoUploadWidget({loading,uploadPhoto} : Props) {

    const [files, setFile] = useState<any>([]);
    const [cropper, setCropper] = useState<Cropper>();

    function onCrop() {
        if (cropper) {
            cropper.getCroppedCanvas().toBlob(blob => uploadPhoto(blob!));
        }
    }

    useEffect(() => {
        return () => {
            files.forEach((file: any) => URL.revokeObjectURL(file.preview)); //clean mem from PhotoWidgetDropzone 
        }
    }, [files])

    return (
        <Grid>
            <Grid.Column width={4}>
                <Header sub color='teal' content='Step 1 -Add Photo' />
                <PhotoWidgetDropzone setFile={setFile} />
            </Grid.Column>

            <Grid.Column width={1} />

            <Grid.Column width={4}>
                <Header sub color='teal' content='Step 2 -Resize Image' />
                {files && files.length > 0 && (
                    <PhotoWidgetCropper setCropper={setCropper} imagePreview={files[0].preview} />
                )}
            </Grid.Column>

            <Grid.Column width={1} />

            <Grid.Column width={4}>
                <Header sub color='teal' content='Step 3 -Preview & Upload' />
                {files && files.length > 0 && <>
                    <div className='img-preview' style={{ minHeight: 200, overflow: 'hidden' }} />
                    <Button.Group widths={2}>
                        <Button loading={loading} onClick={onCrop} positive icon='check' />
                        <Button disabled={loading} onClick={() => setFile([])} icon='close' />
                    </Button.Group>
                </>}

            </Grid.Column>
        </Grid>
    )
}