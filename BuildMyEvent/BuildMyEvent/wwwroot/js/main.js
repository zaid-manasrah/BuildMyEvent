function showSuccessMessage(message) {
    const successMsg = document.getElementById('successMessage');
    if (!successMsg) return;

    successMsg.textContent = message;
    successMsg.style.display = 'block';
    successMsg.style.animation = 'slideInRight 0.3s ease-out';

    setTimeout(() => {
        successMsg.style.animation = 'slideOutRight 0.3s ease-out';
        setTimeout(() => {
            successMsg.style.display = 'none';
            successMsg.style.animation = 'slideInRight 0.3s ease-out';
        }, 300);
    }, 3000);
}

function showSection(sectionId) {
    const sections = document.querySelectorAll('.section');
    sections.forEach(sec => {
        sec.classList.remove('active');
    });

    const target = document.getElementById(sectionId);
    if (target) {
        target.classList.add('active');
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }
}

function handleStartNow() {
    const root = document.getElementById('page-root');
    const isLoggedIn = root && root.getAttribute('data-is-logged-in') === 'true';

    if (isLoggedIn) {
        showSection('templates');
    } else {
        window.location.href = '/Account/Login';
    }
}



function handleOpenAiBuilder() {
    showSection('create-by-ai');

    const chatbotHeader = document.getElementById('chatbot-header');
    const chatbotBody = document.getElementById('chatbot-body');

    if (chatbotHeader && chatbotBody) {
        chatbotBody.style.display = 'block';
    }
}


function handleViewTemplates() {
    window.location.href = '/Account/Login';
}

function handleTemplateSelect(templateId) {
    const card = document.querySelector(`#template-${templateId}`);
    if (!card) return;

    const name = card.querySelector('.card-title')?.textContent || templateId;

    const selectedTemplate = {
        id: templateId,
        name: name,
        url: `${templateId}.html`
    };
    localStorage.setItem('selectedTemplate', JSON.stringify(selectedTemplate));

    const hiddenTemplateInput = document.getElementById('template-key');
    if (hiddenTemplateInput) {
        hiddenTemplateInput.value = templateId;
    }

    showSuccessMessage(`Template "${selectedTemplate.name}" selected successfully!`);
    showSection('customize-template');
}

function initContactForm() {
    const form = document.getElementById('contact-form');
    if (!form) return;

    form.addEventListener('submit', function (e) {
        e.preventDefault();
        showSuccessMessage('Your message has been sent. Thank you!');
        form.reset();
    });
}

function initTypingAnimation() {
    const el = document.getElementById('typing-animation-home');
    if (!el || typeof Typed === 'undefined') return;

    new Typed('#typing-animation-home', {
        strings: [
            'Create your conference website in minutes.',
            'Choose a template, customize, and publish.',
            'Manage registrations with ease.'
        ],
        typeSpeed: 40,
        backSpeed: 25,
        backDelay: 2000,
        loop: true
    });
}



function initChatbot() {
    const chatbotHeader = document.getElementById('chatbot-header');
    const chatbotBody = document.getElementById('chatbot-body');
    const chatbotInput = document.getElementById('chatbot-input');
    const chatbotMessages = document.getElementById('chatbot-messages');

    if (!chatbotHeader || !chatbotBody || !chatbotInput || !chatbotMessages) {
        return;
    }

    chatbotHeader.addEventListener('click', () => {
        chatbotBody.style.display = chatbotBody.style.display === 'none' ? 'block' : 'none';
    });

    function normalizeArabicText(text) {
        return text
            .replace(/[\u0617-\u061A\u064B-\u065F]/g, '') 
            .replace(/[إأآ]/g, 'ا') 
            .replace(/ى/g, 'ي') 
            .replace(/ال/g, ''); 
    }

    const responses = {
        'مرحبا|مرحب|هاي|hello|hi|مراحب|test': {
            ar: 'أهلين! كيف أقدر أساعدك اليوم؟ لو مسجل دخول، جربي اسألي شيء زي "كيف أختار قالب؟" أو "غيّر اسم المؤتمر".',
            en: 'Hey there! How can I help you today? If you’re logged in, try asking something like "How to choose a template?" or "Change the conference name".'
        },
        'كيف اختار قالب|كيف أختار قالب|كيف اختار القالب|كيف أختار القالب|ازاي اختار قالب|كيف اختار template|how to choose template|choose template': {
            ar: 'روحي لقسم Templates من الـ Navbar، اختاري قالب زي Tech Pro أو Creative Spark، واضغطي Select Template.',
            en: 'Go to the Templates section from the Navbar, pick a template like Tech Pro or Creative Spark, and click Select Template.'
        },
        'ايش القوالب|إيش القوالب|ايش القوالب المتوفرة|إيش القوالب المتوفرة|شو القوالب|what templates|available templates': {
            ar: 'فيه قوالب: Tech Pro، Creative Spark، Business Elite، Minimalist، Colorful، Academic. تقدري تشوفيهم في قسم Templates.',
            en: 'Available templates: Tech Pro, Creative Spark, Business Elite, Minimalist, Colorful, Academic. Check them out in the Templates section.'
        },
        'كيف اشوف القالب|كيف أشوف القالب|كيف اشوف قالب|كيف أشوف قالب|ازاي اشوف template|how to preview template|preview template': {
            ar: 'في قسم Templates، اضغطي على Preview عشان تشوفي القالب قبل ما تختاريه.',
            en: 'In the Templates section, click Preview to see the template before selecting it.'
        },
        'كيف اعدل الموقع|كيف أعدل الموقع|كيف اعدل موقع|كيف أعدل موقع|ازاي اعدل الموقع|how to edit site|customize site': {
            ar: 'بعد ما تختاري قالب، روحي لقسم Customize Template وأدخلي بيانات زي اسم المؤتمر واللوجو. أو قولي لي إيش بدك تغيري!',
            en: 'After picking a template, go to Customize Template and enter details like conference name and logo. Or tell me what you want to change!'
        },
        'كيف انشر الموقع|كيف أنشر الموقع|كيف انشر موقع|كيف أنشر موقع|ازاي انشر الموقع|how to publish site|publish site': {
            ar: 'بعد ما تخصّصي القالب في قسم Customize، اضغطي Create Conference Website. هيفتح معاكِ القالب جاهز!',
            en: 'After customizing the template in the Customize section, click Create Conference Website. Your site will be ready!'
        },
        'غيّر اسم المؤتمر|تغيير اسم المؤتمر|غير اسم المؤتمر|غير اسم المؤتمر ل|change conference name|set conference name': handleChangeConferenceName,
        'حط اللوجو|اضافة لوجو|إضافة لوجو|اضف اللوجو|إضافة اللوجو|add logo|set logo': handleAddLogo,
        'غيّر اللون|تغيير اللون|غير اللون|غير اللون ل|change color|set color': handleChangeColor,
        'كيف اسجل دخول|كيف أسجل دخول|ازاي اسجل دخول|how to log in|login': {
            ar: 'روحي لقسم Log In من الـ Navbar، أدخلي الإيميل والباسورد بتوعك، واضغطي Log In.',
            en: 'Go to the Log In section from the Navbar, enter your email and password, and click Log In.'
        },
        'ايش الموقع|إيش الموقع|شو الموقع|ايش هادا الموقع|ايش هذا الموقع|what is this site|about site|what’s this site': {
            ar: 'BuildMyEvent هو موقع يساعدك تنشئي موقع لمؤتمرك بسهولة. اختاري قالب، خصّصيه، وانشريه في خطوات بسيطة!',
            en: 'BuildMyEvent is a platform to easily create a website for your conference. Choose a template, customize it, and publish it in simple steps!'
        },
        'default': {
            ar: 'معلش، مش فاهم سؤالك! جربي شيء زي "كيف أختار القالب؟"، "غيّر اسم المؤتمر لـ Tech Summit"، أو "إيش الموقع؟".',
            en: 'Sorry, I didn’t get that! Try something like "How to choose a template?", "Change the conference name to Tech Summit", or "What is this site?".'
        }
    };

    chatbotInput.addEventListener('keypress', (e) => {
        if (e.key === 'Enter' && chatbotInput.value.trim()) {
            const userMessage = chatbotInput.value.trim();
            appendMessage('You', userMessage);

            const user = JSON.parse(localStorage.getItem('currentUser'));
            const normalizedMessage = normalizeArabicText(userMessage).toLowerCase();
            let response = responses['default'];
            const isEnglish = /[a-zA-Z]/.test(userMessage);

            if (!user && !normalizedMessage.match(/مرحبا|مرحب|هاي|hello|hi/)) {
                const reply = isEnglish
                    ? 'Please log in so I can assist you! Click Log In from the Navbar.'
                    : 'سجّل دخول عشان أقدر أساعدك! اضغطي Log In من الـ Navbar.';
                setTimeout(() => {
                    appendMessage('Bot', reply);
                    showSection('log-in');
                }, 500);
                chatbotInput.value = '';
                return;
            }

            for (let key in responses) {
                if (key === 'default') continue;
                const keywords = key.split('|').map(k => normalizeArabicText(k).toLowerCase());
                if (keywords.some(keyword => normalizedMessage.includes(keyword))) {
                    response = responses[key];
                    break;
                }
            }

            if (typeof response === 'function') {
                response(userMessage);
            } else {
                const reply = isEnglish ? response.en : response.ar;
                setTimeout(() => appendMessage('Bot', reply), 500);
            }

            chatbotInput.value = '';
        }
    });

    function handleChangeConferenceName(message) {
        const user = JSON.parse(localStorage.getItem('currentUser'));
        if (!user) {
            setTimeout(() => {
                appendMessage('Bot', 'سجّل دخول عشان أقدر أساعدك! اضغطي Log In من الـ Navbar.');
                showSection('log-in');
            }, 500);
            return;
        }

        const match = message.match(/(غيّر اسم المؤتمر|تغيير اسم المؤتمر|غير اسم المؤتمر|change conference name|set conference name)\s*(لـ|to)?\s*(.+)/i);
        if (match && match[3]) {
            const newName = match[3].trim();
            const customization = JSON.parse(localStorage.getItem('customization') || '{}');
            customization.eventName = newName;
            localStorage.setItem('customization', JSON.stringify(customization));

            const eventNameInput = document.getElementById('event-name');
            if (eventNameInput) {
                eventNameInput.value = newName;
                appendMessage('Bot', `تمام، غيّرت اسم المؤتمر لـ "${newName}". اضغطي Create Conference Website عشان تطبّقي التغييرات!`);
            } else {
                appendMessage('Bot', `حفظت اسم المؤتمر "${newName}". روحي لقسم Customize Template عشان تطبّقي التغييرات!`);
            }
            showSection('customize-template');
        } else {
            appendMessage(
                'Bot',
                'قولي الاسم الجديد لو سمحتي. مثال: "غيّر اسم المؤتمر لـ Tech Summit 2025" أو "Change conference name to Tech Summit 2025"'
            );
        }
    }

    function handleAddLogo(message) {
        const user = JSON.parse(localStorage.getItem('currentUser'));
        if (!user) {
            setTimeout(() => {
                appendMessage('Bot', 'سجّل دخول عشان أقدر أساعدك! اضغطي Log In من الـ Navbar.');
                showSection('log-in');
            }, 500);
            return;
        }

        appendMessage(
            'Bot',
            'روحي لقسم Customize Template وارفعي اللوجو من هناك. لو عندك اسم ملف معين، قولي لي وأساعدك!'
        );
        showSection('customize-template');
    }

    function handleChangeColor(message) {
        const user = JSON.parse(localStorage.getItem('currentUser'));
        if (!user) {
            setTimeout(() => {
                appendMessage('Bot', 'سجّل دخول عشان أقدر أساعدك! اضغطي Log In من الـ Navbar.');
                showSection('log-in');
            }, 500);
            return;
        }

        const match = message.match(/(غيّر اللون|تغيير اللون|غير اللون|change color|set color)\s*(لـ|to)?\s*(.+)/i);
        if (match && match[3]) {
            const newColor = match[3].trim();
            const customization = JSON.parse(localStorage.getItem('customization') || '{}');
            customization.backgroundColor = newColor;
            localStorage.setItem('customization', JSON.stringify(customization));

            appendMessage(
                'Bot',
                `تمام، حفظت لون الخلفية "${newColor}". روحي لقسم Customize Template واضغطي Create Conference Website عشان تطبّقي التغييرات!`
            );
            showSection('customize-template');
        } else {
            appendMessage(
                'Bot',
                'قولي اللون الجديد لو سمحتي. مثال: "غيّر اللون لـ blue" أو "Change color to #ff0000"'
            );
        }
    }

    function appendMessage(sender, message) {
        const messageDiv = document.createElement('div');
        messageDiv.className = `chatbot-message ${sender.toLowerCase()}`;
        messageDiv.innerHTML = `<strong>${sender}:</strong> ${message}`;
        chatbotMessages.appendChild(messageDiv);
        chatbotMessages.scrollTop = chatbotMessages.scrollHeight;
    }
}



document.addEventListener('DOMContentLoaded', function () {
    initContactForm();
    initTypingAnimation();
    initChatbot();
    showSection('home');
});
