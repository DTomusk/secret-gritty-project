import Stack from "@mui/material/Stack";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";

type PollOptionEditProps = {
    optionId: string;
    optionText: string;
    onSave: (optionId: string, newText: string) => void;
    onDelete?: (optionId: string) => void;
};

export default function PollOptionEdit({ optionId, optionText, onSave, onDelete }: PollOptionEditProps) {
    return (
        <Stack direction="row" spacing={1}>
            <TextField
                label="Option Text"
                variant="outlined"
                size="small"
                value={optionText}
                onChange={(e) => onSave(optionId, e.target.value)}
                sx ={{ flexGrow: 1 }}
            />
            <Button
                variant="contained"
                color="primary"
                onClick={() => onSave(optionId, optionText)}
            >
                Save
            </Button>
            {onDelete && (
                <Button
                    variant="outlined"
                    color="error"
                    onClick={() => onDelete(optionId)}
                >
                    Delete
                </Button>
            )}
        </Stack>
    );
}