import React from 'react';

interface Props {
    name: string;
}
const Test: React.FC<Props> = ({ name }: Props) => <div>{name}</div>;

const App: React.FC = () => {
    return (
        <div>
            <Test name={'dsa'} />
        </div>
    );
};

export default App;
