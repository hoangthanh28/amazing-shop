import React from 'react';

interface Props {
    isOpen: boolean;
    classHolder: string;
    closeModal: () => void;
    title?: string;
    content?: React.ReactNode;
}

const Modal: React.FunctionComponent<Props> = React.memo(
    ({ isOpen, closeModal, content, classHolder, title }) => {
        return (
            <div className="container-modal">
                <div
                    className={`modalSaas ${classHolder} ${isOpen ? 'is-modal-active' : ''}`}
                    data-modal-name="modal-use-change-logo"
                >
                    <div className="backdrop" onClick={closeModal} />
                    <div className="modalSaas__dialog modalSaas__btn--close">
                        <button className="modalSaas__close" onClick={closeModal} />
                        {title && title !== '' && title != null ? <div className="modalSaas__header"><span className="modalSaas__title">{title}</span></div> : null}
                        <div className="modalSaas__content">
                            {content}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
);

Modal.displayName = 'Modal';

export default Modal;
