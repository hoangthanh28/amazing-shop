import React from 'react';
import { VelocityTransitionGroup } from 'velocity-react';

interface Props {
    isShow: boolean;
    className?: string;
}

const Accordion: React.FunctionComponent<Props> = ({ isShow, className = 'accordion', children }) => {
    return (
        <VelocityTransitionGroup enter={{ animation: 'slideDown' }} leave={{ animation: 'slideUp' }} className={className}>
            {isShow ? children : null}
        </VelocityTransitionGroup>
    )
};

Accordion.displayName = 'Accordion';

export default Accordion;